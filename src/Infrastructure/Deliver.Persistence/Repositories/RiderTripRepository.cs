using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RiderTripRepository : BaseRepository<Trip>, IRiderTripRepository
{
    public RiderTripRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Trip?> GetCurrentTripAsync(int userId)
    {
        var activeTripStatus = TripStatusExtensions.GetActiveStatuses();

        return await _dbContext
            .Trips.Include(t => t.PickUpAddress)
            .Include(t => t.DropOffAddress)
            .FirstOrDefaultAsync(
                t => t.RiderId == userId
                     && TripStatusExtensions.GetActiveStatuses().Contains(t.Status)
            );
    }

    public async Task<List<Trip>> GetRiderTrips(int userId, int page, int? size)
    {
        var pageSize = size ?? 10;

        return await _dbContext
            .Set<Trip>()
            .Where(trip => trip.RiderId == userId)
            .OrderBy(trip => trip.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}