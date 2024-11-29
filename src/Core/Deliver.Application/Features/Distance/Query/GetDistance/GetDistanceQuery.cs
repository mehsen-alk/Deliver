using Deliver.Application.Features.Distance.Common.Location;
using MediatR;

namespace Deliver.Application.Features.Distance.Query.GetDistance;

public class GetDistanceQuery : IRequest<DistanceVm>
{
    public required Location Origin { get; set; }
    public required Location Destination { get; set; }
}