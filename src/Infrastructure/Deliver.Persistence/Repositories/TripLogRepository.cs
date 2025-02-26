using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class TripLogRepository : BaseRepository<TripLog>, ITripLogRepository
{
    public TripLogRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TripLog?> GetAcceptedTripLogsAsync(int tripId)
    {
        return await _dbContext
            .TripLogs
            .Where(
                tl => tl.TripId == tripId && tl.Status == TripStatus.OnWayToPickupRider
            )
            .FirstOrDefaultAsync();
    }
}