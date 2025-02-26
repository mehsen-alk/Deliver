using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.RiderTrips.Command.RiderCreateTrip;

public class RiderCreateTripCommandHandler
    : IRequestHandler<RiderCreateTripCommand, RiderCreateTripDto>
{
    private readonly IAsyncRepository<Address> _addressRepository;
    private readonly IMapper _mapper;
    private readonly IRiderTripRepository _riderTripRepository;
    private readonly IAsyncRepository<TripLog> _tripLogRepository;
    private readonly IAsyncRepository<Trip> _tripRepository;

    public RiderCreateTripCommandHandler(
        IAsyncRepository<Trip> tripRepository,
        IMapper mapper,
        IAsyncRepository<Address> addressRepository,
        IAsyncRepository<TripLog> tripLogRepository,
        IRiderTripRepository riderTripRepository
    )
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
        _tripLogRepository = tripLogRepository;
        _riderTripRepository = riderTripRepository;
    }

    public async Task<RiderCreateTripDto> Handle(
        RiderCreateTripCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new RiderCreateTripValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var currentTrip = await _riderTripRepository.GetCurrentTripAsync(command.RiderId);

        if (currentTrip != null)
            throw new DeliverException(DeliverErrorCodes.ActiveTripExists);

        await using var transaction = await _addressRepository.BeginTransactionAsync();

        var pickUp = new Address
        {
            Type = AddressType.PickUp,
            Longitude = command.PickUpAddress.Longitude,
            Latitude = command.PickUpAddress.Latitude,
            UserId = command.RiderId
        };

        pickUp = await _addressRepository.AddAsync(pickUp);

        var dropOff = new Address
        {
            Type = AddressType.DropOff,
            Longitude = command.DropOffAddress.Longitude,
            Latitude = command.DropOffAddress.Latitude,
            UserId = command.RiderId
        };

        dropOff = await _addressRepository.AddAsync(dropOff);

        var trip = new Trip
        {
            RiderId = command.RiderId,
            CalculatedDuration = command.Duration,
            CalculatedDistance = command.Distance,
            Status = TripStatus.Waiting,
            PickUpAddressId = pickUp.Id,
            DropOffAddressId = dropOff.Id
        };

        trip = await _tripRepository.AddAsync(trip);

        var tripLog = new TripLog
        {
            TripId = trip.Id,
            Status = TripStatus.Waiting,
            Note = "trip created by rider.",
            Type = TripLogType.TripCreatedByRider
        };

        await _tripLogRepository.AddAsync(tripLog);

        await transaction.CommitAsync(cancellationToken);

        var response = _mapper.Map<RiderCreateTripDto>(trip);

        return response;
    }
}