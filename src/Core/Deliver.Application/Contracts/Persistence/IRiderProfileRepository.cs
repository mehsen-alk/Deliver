using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IRiderProfileRepository
{
    Task<RiderProfile> GetRiderCurrentProfile(int userId);
}