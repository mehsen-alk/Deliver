using Deliver.Application.Features.RiderProfile.Common;
using MediatR;

namespace Deliver.Application.Features.RiderProfile.Query.GetRiderProfileForRider;

public class GetRiderProfileForRiderQuery : IRequest<RiderProfileVm>
{
    public int UserId { get; set; }
}