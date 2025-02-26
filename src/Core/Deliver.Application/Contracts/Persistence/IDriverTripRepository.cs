using Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverAvailableTrips;
using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IDriverTripRepository : IAsyncRepository<Trip>
{
    Task<List<TripDto>> GetAvailableTrips(int page, int size);
    Task<Trip?> GetCurrentTripAsync(int userId);
}