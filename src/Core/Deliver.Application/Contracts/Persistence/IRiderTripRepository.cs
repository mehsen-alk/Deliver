using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IRiderTripRepository : IAsyncRepository<Trip>
{
    Task<Trip?> GetCurrentTripAsync(int userId);
    Task<List<Trip>> GetRiderTrips(int userId, int page, int? size);
}