using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;
using MediatR;

namespace Deliver.Application.Features.Notification.Command.PostNotificationToken;

public class PostNotificationCommandHandler
    : IRequestHandler<PostNotificationTokenCommand, int>
{
    private readonly IMapper _mapper;
    private readonly INotificationRepository _notificationRepository;

    public PostNotificationCommandHandler(
        IMapper mapper,
        INotificationRepository notificationRepository,
        IUserContextService userContextService
    )
    {
        _mapper = mapper;
        _notificationRepository = notificationRepository;
    }

    public async Task<int> Handle(
        PostNotificationTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        var notificationToken = _mapper.Map<NotificationToken>(request);

        notificationToken = await _notificationRepository.AddAsync(notificationToken);

        return notificationToken.Id;
    }
}