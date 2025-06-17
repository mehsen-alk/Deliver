using Deliver.Application.Features.Finance.Rider.Query.GetRiderPayment;
using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IRiderFinanceRepository : IAsyncRepository<Payment>
{
    public Task<RiderPaymentsVm> GetRiderPayments(int riderId);
}