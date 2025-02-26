using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.DriverAcceptTrip;

public class DriverAcceptTripCommandHandler
    : IRequestHandler<DriverAcceptTripCommand, DriverAcceptTripVm>
{
    private readonly IAsyncRepository<Address> _addressRepository;
    private readonly IAsyncRepository<TripLog> _tripLogRepository;
    private readonly IAsyncRepository<Trip> _tripRepository;

    public DriverAcceptTripCommandHandler(
        IAsyncRepository<Trip> tripRepository,
        IAsyncRepository<TripLog> tripLogRepository,
        IAsyncRepository<Address> addressRepository
    )
    {
        _tripRepository = tripRepository;
        _tripLogRepository = tripLogRepository;
        _addressRepository = addressRepository;
    }

    public async Task<DriverAcceptTripVm> Handle(
        DriverAcceptTripCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new DriverAcceptTripCommandValidator();
        var validationResults = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResults.IsValid) throw new ValidationException(validationResults);

        var trip = await _tripRepository.GetByIdAsync(command.TripId);

        if (trip == null) throw new NotFoundException(nameof(Trip), command.TripId);

        if (trip.DriverId == command.DriverId)
            throw new DeliverException(DeliverErrorCodes.TripAlreadyAccepted);

        if (trip.Status != TripStatus.Waiting)
            throw new DeliverException(
                DeliverErrorCodes.CannotAcceptBecauseTripStatusIsNotValid
            );

        var transaction = await _tripRepository.BeginTransactionAsync();

        trip.Status = TripStatus.OnWayToPickupRider;
        trip.DriverId = command.DriverId;

        var driverLocation = new Address
        {
            Type = AddressType.DriverLocationWhenAcceptOrder,
            Longitude = command.DriverAddress.Longitude,
            Latitude = command.DriverAddress.Latitude,
            UserId = command.DriverId
        };

        driverLocation = await _addressRepository.AddAsync(driverLocation);

        var acceptByDriverLog = new TripLog
        {
            TripId = command.TripId,
            Status = TripStatus.OnWayToPickupRider,
            Type = TripLogType.TripAcceptedByDriver,
            DriverLocationId = driverLocation.Id
        };

        acceptByDriverLog = await _tripLogRepository.AddAsync(acceptByDriverLog);

        await _tripRepository.UpdateAsync(trip);

        await transaction.CommitAsync(cancellationToken);

        var vm = new DriverAcceptTripVm { TripId = trip.Id };

        return vm;
    }
}