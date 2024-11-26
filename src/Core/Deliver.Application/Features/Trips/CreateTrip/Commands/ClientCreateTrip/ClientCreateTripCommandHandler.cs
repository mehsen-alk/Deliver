using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.CreateTrip.Commands.ClientCreateTrip;

public class ClientCreateTripCommandHandler : IRequestHandler<ClientCreateTripCommand, ClientCreateTripResponse>
{
    private readonly IAsyncRepository<Address> _addressRepository;
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Trip> _tripRepository;

    public ClientCreateTripCommandHandler(IAsyncRepository<Trip> tripRepository, IMapper mapper,
        IAsyncRepository<Address> addressRepository)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<ClientCreateTripResponse> Handle(ClientCreateTripCommand command,
        CancellationToken cancellationToken)
    {
        var response = new ClientCreateTripResponse();

        var validator = new ClientCreateTripValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var pickUp = new Address
        {
            Type = AddressType.PickUp,
            Longitude = command.PickUpAddress.Longitude,
            Latitude = command.PickUpAddress.Latitude,
            UserId = command.ClientId
        };

        pickUp = await _addressRepository.AddAsync(pickUp);

        var dropOff = new Address
        {
            Type = AddressType.PickUp,
            Longitude = command.PickUpAddress.Longitude,
            Latitude = command.PickUpAddress.Latitude,
            UserId = command.ClientId
        };

        dropOff = await _addressRepository.AddAsync(dropOff);

        var trip = new Trip
        {
            ClientId = 1,
            CalculatedDuration = command.Duration,
            CalculatedDistance = command.Distance,
            Status = TripStatus.Waiting,
            PickUpAddressId = pickUp.Id,
            DropOfAddressId = dropOff.Id
        };

        trip = await _tripRepository.AddAsync(trip);
        response.Data = _mapper.Map<ClientCreateTripDto>(trip);

        return response;
    }
}