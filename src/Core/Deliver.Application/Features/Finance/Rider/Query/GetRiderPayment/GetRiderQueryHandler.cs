using Deliver.Application.Contracts.Persistence;
using MediatR;

namespace Deliver.Application.Features.Finance.Rider.Query.GetRiderPayment;

public class GetRiderQueryHandler : IRequestHandler<GetRiderPaymentQuery, RiderPaymentsVm>

{
    private readonly IRiderFinanceRepository _riderFinanceRepository;

    public GetRiderQueryHandler(IRiderFinanceRepository riderFinanceRepository)
    {
        _riderFinanceRepository = riderFinanceRepository;
    }

    public async Task<RiderPaymentsVm> Handle(
        GetRiderPaymentQuery request,
        CancellationToken cancellationToken
    )
    {
        var vm = await _riderFinanceRepository.GetRiderPayments(request.UserId);

        return vm;
    }
}