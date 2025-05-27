using Deliver.Application.Contracts.Persistence;
using MediatR;

namespace Deliver.Application.Features.Notification.Command.
    DeleteNotificationTokenByDeviceId;

public class DeleteNotificationTokenByDeviceIdCommandHandler
    : IRequestHandler<DeleteNotificationTokenByDeviceIdCommand, List<int>>
{
    private readonly INotificationRepository _notificationRepository;

    public DeleteNotificationTokenByDeviceIdCommandHandler(
        INotificationRepository notificationRepository
    )
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<List<int>> Handle(
        DeleteNotificationTokenByDeviceIdCommand request,
        CancellationToken cancellationToken
    )
    {
        var deleteNotifications =
            await _notificationRepository.DeleteDeviceTokens(request.DeviceId);

        var deleteNotificationsIds = deleteNotifications.Select(x => x.Id).ToList();

        return deleteNotificationsIds;
    }
}