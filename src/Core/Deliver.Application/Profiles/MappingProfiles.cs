using AutoMapper;
using Deliver.Application.Dto.Address;
using Deliver.Application.Features.DriverProfile.Common;
using Deliver.Application.Features.Trips.Commands.RiderCreateTrip;
using Deliver.Application.Features.Trips.Common.AddressRequest;
using Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;
using Deliver.Application.Features.Trips.Query.GetDriverCurrentTrip;
using Deliver.Application.Features.Trips.Query.GetRiderCurrentTrip;
using Deliver.Domain.Entities;

namespace Deliver.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressRequest>().ReverseMap();
        CreateMap<Address, AddressDto>();

        CreateMap<Trip, RiderCreateTripDto>();
        CreateMap<Trip, RiderCreateTripCommand>();
        CreateMap<RiderCreateTripCommand, RiderCreateTripRequest>().ReverseMap();
        CreateMap<Trip, RiderCurrentTripVm>();

        CreateMap<Trip, TripDto>();

        CreateMap<Trip, DriverCurrentTripVm>();

        CreateMap<DriverProfile, DriverProfileVm>();
    }
}