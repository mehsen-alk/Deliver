using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.CreateTrip.Commands.RiderCreateTrip;

public class
    RiderCreateTripCommandHandler : IRequestHandler<RiderCreateTripCommand,
    RiderCreateTripResponse>
{
    private readonly IAsyncRepository<Address> _addressRepository;
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Trip> _tripRepository;

    public RiderCreateTripCommandHandler(
        IAsyncRepository<Trip> tripRepository,
        IMapper mapper,
        IAsyncRepository<Address> addressRepository
    )
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<RiderCreateTripResponse> Handle(
        RiderCreateTripCommand command,
        CancellationToken cancellationToken
    )
    {
        var response = new RiderCreateTripResponse();

        var validator = new RiderCreateTripValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

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
            Type = AddressType.PickUp,
            Longitude = command.DropOfAddress.Longitude,
            Latitude = command.DropOfAddress.Latitude,
            UserId = command.RiderId
        };

        dropOff = await _addressRepository.AddAsync(dropOff);

        var trip = new Trip
        {
            CalculatedDuration = command.Duration,
            CalculatedDistance = command.Distance,
            Status = TripStatus.Waiting,
            PickUpAddressId = pickUp.Id,
            DropOfAddressId = dropOff.Id
        };

        trip = await _tripRepository.AddAsync(trip);
        response.Data = _mapper.Map<RiderCreateTripDto>(trip);

        return response;
    }
}