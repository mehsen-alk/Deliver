using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RiderProfileRepository : IRiderProfileRepository
{
    private readonly DeliverDbContext _dbContext;

    public RiderProfileRepository(DeliverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RiderProfile> GetRiderCurrentProfile(int userId)
    {
        return await _dbContext.RidersProfile.FirstAsync(
            profile => profile.UserId == userId && profile.Status == ProfileStatus.Current
        );
    }
}