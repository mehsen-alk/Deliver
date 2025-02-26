using Deliver.Application.Features.Profiles.DriverProfile.Common;
using MediatR;

namespace Deliver.Application.Features.Profiles.DriverProfile.Query.
    GetDriverProfileForDriver;

public class GetDriverProfileForDriverQuery : IRequest<DriverProfileVm>
{
    public int UserId { get; set; }
}