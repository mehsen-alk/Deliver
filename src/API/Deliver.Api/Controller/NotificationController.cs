using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Notification.Command.PostNotificationToken;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller;

[Route("v1/[controller]")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public NotificationController(
        IMapper mapper,
        IUserContextService userContextService,
        INotificationRepository notificationRepository,
        IMediator mediator
    )
    {
        _mapper = mapper;
        _userContextService = userContextService;
        _mediator = mediator;
    }

    [HttpPost("notification")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status201Created)]
    public async Task<ActionResult> AddNotificationToken(
        [FromBody] PostNotificationTokenRequest request
    )
    {
        var userId = _userContextService.GetUserId();

        var command = _mapper.Map<PostNotificationTokenCommand>(request);
        command.UserId = userId;

        var result = await _mediator.Send(command);

        return Ok(BaseResponse<string>.CreatedSuccessfully(data: result.ToString()));
    }
}