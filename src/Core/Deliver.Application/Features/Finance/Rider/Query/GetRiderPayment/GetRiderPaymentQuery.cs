using MediatR;

namespace Deliver.Application.Features.Finance.Rider.Query.GetRiderPayment;

public class GetRiderPaymentQuery : IRequest<RiderPaymentsVm>
{
    public int UserId { get; set; }
}