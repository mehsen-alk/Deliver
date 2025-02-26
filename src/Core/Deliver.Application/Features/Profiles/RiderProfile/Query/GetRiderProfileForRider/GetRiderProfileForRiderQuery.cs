using Deliver.Application.Features.Profiles.RiderProfile.Common;
using MediatR;

namespace Deliver.Application.Features.Profiles.RiderProfile.Query.
    GetRiderProfileForRider;

public class GetRiderProfileForRiderQuery : IRequest<RiderProfileVm>
{
    public int UserId { get; set; }
}