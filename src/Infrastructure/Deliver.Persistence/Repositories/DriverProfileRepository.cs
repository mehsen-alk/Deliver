using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class DriverProfileRepository : IDriverProfileRepository
{
    private readonly DeliverDbContext _dbContext;

    public DriverProfileRepository(DeliverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DriverProfile> GetDriverCurrentProfile(int userId)
    {
        return await _dbContext.DriversProfile.FirstAsync(
            profile => profile.UserId == userId && profile.Status == ProfileStatus.Current
        );
    }
}