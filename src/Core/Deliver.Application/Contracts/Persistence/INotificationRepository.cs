using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface INotificationRepository
{
    Task<NotificationToken> AddAsync(NotificationToken entity);
    Task<NotificationToken?> DeleteTokenAsync(string token);
    Task<IReadOnlyList<NotificationToken>> DeleteUserTokens(int userId);
    Task<IReadOnlyList<NotificationToken>> DeleteDeviceTokens(string deviceId);
    Task<IReadOnlyList<NotificationToken>> GetUserTokens(int userId);
}