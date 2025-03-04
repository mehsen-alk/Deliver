using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Finance.Driver.Query.GetDriverProfits;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DriverFinanceRepository : BaseRepository<Payment>, IDriverFinanceRepository
{
    public DriverFinanceRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<DriverEarningsVm> GetDriverProfits(int driverId)
    {
        var query = _dbContext
            .Payments.Where(p => p.ToUserId == driverId && p.Status == PaymentStatus.Paid)
            .OrderByDescending(p => p.CreatedDate);

        var totalEarnings =
            await query.SumAsync(p => p.Amount - p.Amount * p.CompanyCommission);

        var payments = await query
            .Select(
                p => new PaymentRecordVm
                {
                    CreatedDate = p.CreatedDate,
                    Amount = p.Amount,
                    CompanyCommission = p.CompanyCommission,
                    PaymentMethod = p.PaymentMethod,
                    PaymentType = p.Type,
                    TripId = p.TripId
                }
            )
            .ToListAsync();

        return new DriverEarningsVm
        {
            TotalEarnings = totalEarnings,
            Payments = payments
        };
    }
}