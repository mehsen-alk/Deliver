using Deliver.Application.Features.DriverProfile.Common;
using MediatR;

namespace Deliver.Application.Features.DriverProfile.Query.GetDriverProfileForDriver;

public class GetDriverProfileForDriverQuery : IRequest<DriverProfileVm>
{
    public int UserId { get; set; }
}