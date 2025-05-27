using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly DeliverDbContext _dbContext;

    public NotificationRepository(DeliverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NotificationToken> AddAsync(NotificationToken entity)
    {
        var existingToken = await _dbContext
            .NotificationTokens
            .Where(t => t.UserId == entity.UserId && t.DeviceId == entity.DeviceId)
            .FirstOrDefaultAsync();

        if (existingToken != null)
        {
            // token already exist
            if (existingToken.Token == entity.Token) return existingToken;

            // update the old token
            existingToken.Token = entity.Token;
            await _dbContext.SaveChangesAsync();
            return existingToken;
        }

        // token is new, create it
        await _dbContext.NotificationTokens.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<NotificationToken?> DeleteTokenAsync(string token)
    {
        var notificationToken = await _dbContext
            .NotificationTokens
            .Where(notificationToken => notificationToken.Token == token)
            .FirstOrDefaultAsync();

        if (notificationToken != null)
        {
            _dbContext.NotificationTokens.Remove(notificationToken);

            await _dbContext.SaveChangesAsync();

            return notificationToken;
        }

        return null;
    }

    public async Task<IReadOnlyList<NotificationToken>> DeleteUserTokens(int userId)
    {
        var tokens = await _dbContext
            .NotificationTokens.Where(token => token.UserId == userId)
            .ToListAsync();

        _dbContext.NotificationTokens.RemoveRange(tokens);

        await _dbContext.SaveChangesAsync();

        return tokens;
    }

    public async Task<IReadOnlyList<NotificationToken>> DeleteDeviceTokens(
        string deviceId
    )
    {
        var tokens = await _dbContext
            .NotificationTokens.Where(token => token.DeviceId == deviceId)
            .ToListAsync();

        _dbContext.NotificationTokens.RemoveRange(tokens);

        await _dbContext.SaveChangesAsync();

        return tokens;
    }

    public async Task<IReadOnlyList<NotificationToken>> GetUserTokens(int userId)
    {
        var tokens = await _dbContext
            .NotificationTokens.Where(token => token.UserId == userId)
            .ToListAsync();

        return tokens;
    }
}