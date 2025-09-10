using Deliver.Domain.Entities;
using Deliver.Domain.Enums;

namespace Deliver.Application.Contracts.Persistence;

public interface IAdminTripRepository : IAsyncRepository<Trip>
{
    Task<List<Trip>> FilterTrip(int page, int size, int? tripId, TripStatus? tripStatus);
}