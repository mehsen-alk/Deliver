using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Dto.Address;
using Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverAvailableTrips;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DriverTripRepository : BaseRepository<Trip>, IDriverTripRepository
{
    public DriverTripRepository(DeliverDbContext dbContext) : base(dbContext)
    {
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
                        Latitude = t.DropOffAddress.Latitude,
                        Longitude = t.DropOffAddress.Longitude
                    },
                    CalculatedDistance = t.CalculatedDistance,
                    CalculatedDuration = t.CalculatedDuration,
                    CreatedDate = t.CreatedDate
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
            .Include(t => t.DropOffAddress)
            .FirstOrDefaultAsync(
                t => t.DriverId == userId && activeTripStatus.Contains(t.Status)
            );
    }

    public async Task<List<Trip>> GetDriverTrips(int userId, int page, int size)
    {
        return await _dbContext
            .Set<Trip>()
            .Where(trip => trip.DriverId == userId)
            .OrderBy(trip => trip.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync();
    }
}