using Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;

namespace Deliver.Application.Contracts.Persistence;

public interface IDriverTripRepository
{
    Task<List<TripDto>> GetAvailableTrips(int page, int size);
}