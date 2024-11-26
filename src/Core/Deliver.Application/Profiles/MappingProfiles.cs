using AutoMapper;
using Deliver.Application.Features.Trips.Common.AddressRequest;
using Deliver.Application.Features.Trips.CreateTrip.Commands.ClientCreateTrip;
using Deliver.Domain.Entities;

namespace Deliver.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressRequest>().ReverseMap();

        CreateMap<Trip, ClientCreateTripDto>();
        CreateMap<Trip, ClientCreateTripCommand>();
        CreateMap<ClientCreateTripCommand, ClientCreateTripRequest>().ReverseMap();
    }
}