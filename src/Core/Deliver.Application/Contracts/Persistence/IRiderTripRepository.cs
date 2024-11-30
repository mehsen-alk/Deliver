using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IRiderTripRepository
{
    Task<Trip?> GetCurrentTripAsync(int userId);
}