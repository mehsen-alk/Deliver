using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DriverProfileRepository
    : BaseRepository<DriverProfile>, IDriverProfileRepository
{
    public DriverProfileRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<DriverProfile> GetDriverCurrentProfile(int userId)
    {
        return await _dbContext.DriversProfile.FirstAsync(
            profile => profile.UserId == userId && profile.Status == ProfileStatus.Current
        );
    }
}