namespace Deliver.Application.Models.Notification;

public class CreateNotificationTokenRequest
{
    public string Token { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
}