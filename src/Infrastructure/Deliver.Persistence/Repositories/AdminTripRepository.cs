using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AdminTripRepository : BaseRepository<Trip>, IAdminTripRepository
{
    public AdminTripRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Trip>> FilterTrip(
        int page,
        int size,
        int? tripId,
        TripStatus? tripStatus
    )
    {
        var query = _dbContext.Trips.AsQueryable();

        if (tripId.HasValue) query = query.Where(t => t.Id == tripId.Value);

        if (tripStatus.HasValue) query = query.Where(t => t.Status == tripStatus.Value);

        query = query.OrderByDescending(t => t.CreatedDate);

        var skip = (page - 1) * size;
        query = query.Skip(skip).Take(size);

        return await query.ToListAsync();
    }
}