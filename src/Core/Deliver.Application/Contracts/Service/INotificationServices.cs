using Deliver.Application.Models.Notification;

namespace Deliver.Application.Contracts.Service;

public interface INotificationServices
{
    public Task<string> SendNotificationAsync(NotificationRequest request);
}