using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RiderTripRepository : IRiderTripRepository
{
    private readonly DeliverDbContext _dbContext;

    public RiderTripRepository(DeliverDbContext dbContext)
    {
        _dbContext = dbContext;
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
}