using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface ITripLogRepository : IAsyncRepository<TripLog>
{
    Task<TripLog?> GetAcceptedTripLogsAsync(int tripId);
}