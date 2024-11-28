using AutoMapper;
using Deliver.Application.Features.Trips.Commands.RiderCreateTrip;
using Deliver.Application.Features.Trips.Common.AddressRequest;
using Deliver.Domain.Entities;

namespace Deliver.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressRequest>().ReverseMap();

        CreateMap<Trip, RiderCreateTripDto>();
        CreateMap<Trip, RiderCreateTripCommand>();
        CreateMap<RiderCreateTripCommand, RiderCreateTripRequest>().ReverseMap();
    }
}