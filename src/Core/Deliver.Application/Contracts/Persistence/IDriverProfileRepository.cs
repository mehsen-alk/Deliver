using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IDriverProfileRepository
{
    Task<DriverProfile> GetDriverCurrentProfile(int userId);
}