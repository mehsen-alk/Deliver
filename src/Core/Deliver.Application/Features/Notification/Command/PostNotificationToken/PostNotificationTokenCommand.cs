using MediatR;

namespace Deliver.Application.Features.Notification.Command.PostNotificationToken;

public class PostNotificationTokenCommand : IRequest<int>
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
}