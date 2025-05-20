using Deliver.Application.Models.Notification;

namespace Deliver.Application.Contracts.Persistence;

public interface INotificationServices
{
    public Task<string> SendNotificationAsync(NotificationRequest request);
}