using AutoMapper;
using Deliver.Application.Dto.Address;
using Deliver.Application.Features.Notification.Command;
using Deliver.Application.Features.Profiles.DriverProfile.Commands.EditProfileByDriver;
using Deliver.Application.Features.Profiles.DriverProfile.Common;
using Deliver.Application.Features.Profiles.RiderProfile.Commands.EditProfileByRider;
using Deliver.Application.Features.Profiles.RiderProfile.Common;
using Deliver.Application.Features.Trips.Common.AddressRequest;
using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverAvailableTrips;
using Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverCurrentTrip;
using Deliver.Application.Features.Trips.RiderTrips.Command.RiderCreateTrip;
using Deliver.Application.Features.Trips.RiderTrips.Query.GetRiderCurrentTrip;
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
        CreateMap<Trip, TripHistoryVm>();

        CreateMap<DriverProfile, DriverProfileVm>();
        CreateMap<EditProfileByDriverCommand, DriverProfile>();

        CreateMap<RiderProfile, RiderProfileVm>();
        CreateMap<EditProfileByRiderCommand, RiderProfile>();

        CreateMap<PostNotificationTokenRequest, PostNotificationTokenCommand>();
        CreateMap<PostNotificationTokenCommand, NotificationToken>();
    }
}