namespace Deliver.Application.Features.Notification.Command.PostNotificationToken;

public class PostNotificationTokenRequest
{
    public string Token { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
}