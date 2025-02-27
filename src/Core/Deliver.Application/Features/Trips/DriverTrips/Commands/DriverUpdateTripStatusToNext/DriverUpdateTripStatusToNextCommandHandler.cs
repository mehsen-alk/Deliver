using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.
    DriverUpdateTripStatusToNext;

public class DriverUpdateTripStatusToNextCommandHandler
    : IRequestHandler<DriverUpdateTripStatusToNextCommand, int>
{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly ITripLogRepository _tripLogRepository;

    public DriverUpdateTripStatusToNextCommandHandler(
        IDriverTripRepository driverTripRepository,
        ITripLogRepository tripLogRepository
    )
    {
        _driverTripRepository = driverTripRepository;
        _tripLogRepository = tripLogRepository;
    }

    public async Task<int> Handle(
        DriverUpdateTripStatusToNextCommand request,
        CancellationToken cancellationToken
    )
    {
        var driverTrip =
            await _driverTripRepository.GetCurrentTripAsync(request.DriverId);

        if (driverTrip == null)
            throw new DeliverException(DeliverErrorCodes.YouDontHaveAnActiveTrip);

        try
        {
            driverTrip.Status = driverTrip.Status.NextStatus();
        }
        catch
        {
            throw new DeliverException(
                DeliverErrorCodes.TripStatusCantBeUpdatedToNextStatus
            );
        }

        var log = new TripLog
        {
            Type = TripLogType.TripCancelledByDriver,
            Note = "trip updated by driver",
            Status = driverTrip.Status,
            TripId = driverTrip.Id
        };

        var transaction = await _driverTripRepository.BeginTransactionAsync();
        try
        {
            await _driverTripRepository.UpdateAsync(driverTrip);
            await _tripLogRepository.AddAsync(log);

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