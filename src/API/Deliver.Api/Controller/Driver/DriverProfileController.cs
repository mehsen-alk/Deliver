using Deliver.Application.Contracts.Identity;
using Deliver.Application.Exceptions;
using Deliver.Application.Features.Profiles.DriverProfile.Commands.EditProfileByDriver;
using Deliver.Application.Features.Profiles.DriverProfile.Commands.VehicleRegisterByDriver;
using Deliver.Application.Features.Profiles.DriverProfile.Common;
using Deliver.Application.Features.Profiles.DriverProfile.Query.GetDriverProfileForDriver;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Driver;

[ApiController]
[Route("v1/driver/profile")]
[Authorize(Roles = "Driver")]
public class DriverProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public DriverProfileController(
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
    public async Task<ActionResult<BaseResponse<DriverProfileVm>>> GetProfile()
    {
        var userId = _userContextService.GetUserId();
        var query = new GetDriverProfileForDriverQuery { UserId = userId };

        var profile = await _mediator.Send(query);

        if (profile == null)
            throw new NotFoundException("No profile found");

        var response = BaseResponse<DriverProfileVm>.FetchedSuccessfully(data: profile);

        return Ok(response);
    }

    [HttpPut("registerVehicle")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<BaseResponse<object>>> RegisterVehicle(
        [FromBody] VehicleRegisterByDriverRequest request
    )
    {
        var profileId = await _userContextService.GetDriverProfileId();
        var query = new VehicleRegisterByDriverCommand
        {
            LicenseImage = request.LicenseImage,
            ProfileId = profileId,
            VehicleImage = request.VehicleImage
        };

        await _mediator.Send(query);

        var response = BaseResponse<object>.UpdatedSuccessfully(data: "updated");

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<BaseResponse<object>>> UpdateProfile(
        [FromBody] EditProfileByDriverRequest request
    )
    {
        var userId = _userContextService.GetUserId();
        var profileId = await _userContextService.GetDriverProfileId();

        var query = new EditProfileByDriverCommand
        {
            LicenseImage = request.LicenseImage,
            ProfileId = profileId,
            VehicleImage = request.VehicleImage,
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