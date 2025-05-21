using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Models.Notification;
using Deliver.Application.Responses;
using Deliver.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller;

[Route("v1/[controller]")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserContextService _userContextService;

    public NotificationController(
        IMapper mapper,
        IAuthenticationService authenticationService,
        IUserContextService userContextService,
        INotificationRepository notificationRepository
    )
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
        _userContextService = userContextService;
        _notificationRepository = notificationRepository;
    }

    [HttpPost("notification")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status201Created)]
    public async Task<ActionResult> AddNotificationToken(
        [FromBody] CreateNotificationTokenRequest request
    )
    {
        var userId = _userContextService.GetUserId();

        var notificationToken = _mapper.Map<NotificationToken>(request);
        notificationToken.UserId = userId;

        notificationToken = await _notificationRepository.AddAsync(notificationToken);

        return Ok(
            BaseResponse<string>.CreatedSuccessfully(
                data: notificationToken.Id.ToString()
            )
        );
    }
}