using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Dto.Address;
using Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DriverTripRepository : IDriverTripRepository
{
    private readonly DeliverDbContext _dbContext;

    public DriverTripRepository(DeliverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TripDto>> GetAvailableTrips(int page, int size)
    {
        var trips = await _dbContext
            .Trips.Where(t => t.Status == TripStatus.Waiting)
            .OrderBy(t => t.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .Select(
                t => new TripDto
                {
                    Id = t.Id,
                    PickUpAddress = new AddressDto
                    {
                        Latitude = t.PickUpAddress.Latitude,
                        Longitude = t.PickUpAddress.Longitude
                    },
                    DropOffAddress = new AddressDto
                    {
                        Latitude = t.DropOfAddress.Latitude,
                        Longitude = t.DropOfAddress.Longitude
                    },
                    CalculatedDistance = t.CalculatedDistance,
                    CalculatedDuration = t.CalculatedDuration,
                    CreatedAt = t.CreatedDate
                }
            )
            .ToListAsync();

        return trips;
    }

    public async Task<Trip?> GetCurrentTripAsync(int userId)
    {
        var activeTripStatus = TripStatusExtensions.GetActiveStatuses();

        return await _dbContext
            .Trips.Include(t => t.PickUpAddress)
            .Include(t => t.DropOfAddress)
            .FirstOrDefaultAsync(
                t => t.DriverId == userId
                     && TripStatusExtensions.GetActiveStatuses().Contains(t.Status)
            );
    }
}