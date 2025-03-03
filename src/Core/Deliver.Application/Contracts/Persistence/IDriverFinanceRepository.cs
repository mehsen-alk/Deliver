using Deliver.Application.Features.Finance.Driver.Query.GetDriverProfits;
using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IDriverFinanceRepository : IAsyncRepository<Payment>
{
    public Task<DriverEarningsVm> GetDriverProfits(int driverId);
}