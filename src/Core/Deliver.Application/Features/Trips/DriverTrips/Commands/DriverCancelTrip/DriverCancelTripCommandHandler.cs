using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.DriverCancelTrip;

public class DriverCancelTripCommandHandler
    : IRequestHandler<DriverCancelTripCommand, int>
{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly ITripLogRepository _tripLogRepository;

    public DriverCancelTripCommandHandler(
        IDriverTripRepository driverTripRepository,
        ITripLogRepository tripLogRepository
    )
    {
        _driverTripRepository = driverTripRepository;
        _tripLogRepository = tripLogRepository;
    }

    public async Task<int> Handle(
        DriverCancelTripCommand request,
        CancellationToken cancellationToken
    )
    {
        var driverTrip = await _driverTripRepository.GetCurrentTripAsync(request.UserId);

        if (driverTrip == null)
            throw new DeliverException(DeliverErrorCodes.YouDontHaveAnActiveTrip);

        if (driverTrip.Status > TripStatus.Waiting)
        {
            var acceptTripLog =
                await _tripLogRepository.GetAcceptedTripLogsAsync(driverTrip.Id);

            if (acceptTripLog == null)
                throw new DeliverException(DeliverErrorCodes.YouDontHaveAnActiveTrip);

            if ((DateTime.UtcNow - acceptTripLog.CreatedDate).TotalMinutes > 5)
                throw new DeliverException(
                    DeliverErrorCodes.YouHaveExceededTheTimeAllowedToCancelTrip
                );
        }

        driverTrip.Status = TripStatus.Waiting;
        driverTrip.DriverId = null;

        var cancelTripLog = new TripLog
        {
            Type = TripLogType.TripCancelledByDriver,
            Note = "trip cancelled by driver",
            Status = TripStatus.Cancelled,
            TripId = driverTrip.Id
        };

        var transaction = await _driverTripRepository.BeginTransactionAsync();
        try
        {
            await _driverTripRepository.UpdateAsync(driverTrip);
            await _tripLogRepository.AddAsync(cancelTripLog);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return driverTrip.Id;
    }
}