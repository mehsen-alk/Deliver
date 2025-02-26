using Deliver.Application.Contracts.Identity;
using Deliver.Application.Exceptions;
using Deliver.Application.Features.RiderProfile.Commands.EditProfileByRider;
using Deliver.Application.Features.RiderProfile.Common;
using Deliver.Application.Features.RiderProfile.Query.GetRiderProfileForRider;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Rider;

[ApiController]
[Route("v1/Rider/profile")]
[Authorize(Roles = "Rider")]
public class RiderProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public RiderProfileController(
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
        _userContextService = userContextService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<BaseResponse<RiderProfileVm>>> GetProfile()
    {
        var userId = _userContextService.GetUserId();
        var query = new GetRiderProfileForRiderQuery { UserId = userId };

        var profile = await _mediator.Send(query);

        if (profile == null)
            throw new NotFoundException("No profile found");

        var response = BaseResponse<RiderProfileVm>.FetchedSuccessfully(data: profile);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<BaseResponse<object>>> UpdateProfile(
        [FromBody] EditProfileByRiderRequest request
    )
    {
        var userId = _userContextService.GetUserId();
        var profileId = await _userContextService.GetRiderProfileId();

        var query = new EditProfileByRiderCommand
        {
            ProfileId = profileId,
            Name = request.Name,
            Phone = request.Phone,
            UserId = userId,
            ProfileImage = request.ProfileImage
        };

        await _mediator.Send(query);

        var response = BaseResponse<object>.UpdatedSuccessfully(data: "updated");

        return Ok(response);
    }
}