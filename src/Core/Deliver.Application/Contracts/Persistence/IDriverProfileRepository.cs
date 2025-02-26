using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IDriverProfileRepository : IAsyncRepository<DriverProfile>
{
    Task<DriverProfile> GetDriverCurrentProfile(int userId);
}