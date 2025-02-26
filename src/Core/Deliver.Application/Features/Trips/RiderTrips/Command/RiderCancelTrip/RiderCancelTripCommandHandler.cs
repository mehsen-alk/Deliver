using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.RiderTrips.Command.RiderCancelTrip;

public class RiderCancelTripCommandHandler : IRequestHandler<RiderCancelTripCommand, int>
{
    private readonly IRiderTripRepository _riderTripRepository;
    private readonly IAsyncRepository<TripLog> _tripLogRepository;
    private readonly IAsyncRepository<Trip> _tripRepository;

    public RiderCancelTripCommandHandler(
        IRiderTripRepository riderTripRepository,
        IAsyncRepository<Trip> tripRepository,
        IAsyncRepository<TripLog> tripLogRepository
    )
    {
        _riderTripRepository = riderTripRepository;
        _tripRepository = tripRepository;
        _tripLogRepository = tripLogRepository;
    }

    public async Task<int> Handle(
        RiderCancelTripCommand request,
        CancellationToken cancellationToken
    )
    {
        var riderTrip = await _riderTripRepository.GetCurrentTripAsync(request.UserId);

        if (riderTrip == null)
            throw new DeliverException(DeliverErrorCodes.YouDontHaveAnActiveTrip);

        if (riderTrip.Status > TripStatus.Waiting)
            if ((DateTime.UtcNow - riderTrip.CreatedDate).TotalMinutes > 5)
                throw new DeliverException(
                    DeliverErrorCodes.YouHaveExceededTheTimeAllowedToCancelTrip
                );

        riderTrip.Status = TripStatus.Cancelled;

        var cancelTripLog = new TripLog
        {
            Type = TripLogType.TripCancelledByRider,
            Note = "trip cancelled by rider",
            Status = TripStatus.Cancelled,
            TripId = riderTrip.Id
        };

        var transaction = await _tripRepository.BeginTransactionAsync();
        try
        {
            await _tripRepository.UpdateAsync(riderTrip);
            await _tripLogRepository.AddAsync(cancelTripLog);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return cancelTripLog.Id;
    }
}