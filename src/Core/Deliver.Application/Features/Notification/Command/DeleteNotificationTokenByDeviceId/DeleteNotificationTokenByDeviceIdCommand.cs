using MediatR;

namespace Deliver.Application.Features.Notification.Command.
    DeleteNotificationTokenByDeviceId;

public class DeleteNotificationTokenByDeviceIdCommand : IRequest<List<int>>
{
    public DeleteNotificationTokenByDeviceIdCommand(string deviceId)
    {
        DeviceId = deviceId;
    }

    public string DeviceId { get; set; }
}