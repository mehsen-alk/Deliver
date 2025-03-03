using MediatR;

namespace Deliver.Application.Features.Finance.Driver.Query.GetDriverProfits;

public class GetDriverProfitsQuery : IRequest<DriverEarningsVm>
{
    public int UserId { get; set; }
}