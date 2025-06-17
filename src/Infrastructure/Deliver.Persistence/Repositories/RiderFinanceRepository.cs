using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Finance.Rider.Query.GetRiderPayment;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RiderFinanceRepository : BaseRepository<Payment>, IRiderFinanceRepository
{
    public RiderFinanceRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RiderPaymentsVm> GetRiderPayments(int riderId)
    {
        var query = _dbContext
            .Payments
            .Where(p => p.FromUserId == riderId && p.Status == PaymentStatus.Paid)
            .OrderByDescending(p => p.CreatedDate);

        var payments = await query
            .Select(
                p => new RiderPaymentRecordVm
                {
                    CreatedDate = p.CreatedDate,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentType = p.Type,
                    TripId = p.TripId
                }
            )
            .ToListAsync();

        return new RiderPaymentsVm { Payments = payments };
    }
}