using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Trips.Commands.DriverAcceptTrip;
using Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;
using Deliver.Application.Features.Trips.Query.GetDriverCurrentTrip;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Driver;

[ApiController]
[Route("v1/driver/trip")]
[Authorize(Roles = "Driver")]
public class DriverTripController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public DriverTripController(
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
        _userContextService = userContextService;
    }

    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse<GetDriverAvailableTripsQueryVm>>>
        GetCurrentTrip([FromQuery] GetDriverAvailableTripsQuery request)
    {
        var data = await _mediator.Send(request);

        var response =
            BaseResponse<GetDriverAvailableTripsQueryVm>.FetchedSuccessfully(data: data);

        return Ok(response);
    }

    /// <response code="404">There is no Current Trip</response>
    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponse<DriverCurrentTripVm>>> GetCurrentTrip()
    {
        var query =
            new GetDriverCurrentTripQuery { DriverId = _userContextService.GetUserId() };

        var data = await _mediator.Send(query);

        if (data == null)
        {
            var notFoundResponse = new BaseResponse<string>
            {
                Message = "no current trip.",
                StatusCode = StatusCodes.Status404NotFound
            };

            return NotFound(notFoundResponse);
        }

        var response = BaseResponse<DriverCurrentTripVm>.FetchedSuccessfully(data: data);

        return Ok(response);
    }

    /// <response code="404">There is no Current Trip</response>
    [HttpPut("accept")]
    [ProducesResponseType(
        typeof(BaseResponse<DriverAcceptTripVm>),
        StatusCodes.Status202Accepted
    )]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponse<DriverAcceptTripVm>>> AcceptTrip(
        DriverAcceptTripRequest request
    )
    {
        var query = new DriverAcceptTripCommand
        {
            DriverId = _userContextService.GetUserId(),
            DriverAddress = request.DriverAddress,
            TripId = request.TripId
        };

        var data = await _mediator.Send(query);

        var response = BaseResponse<DriverAcceptTripVm>.UpdatedSuccessfully(data: data);

        return Accepted(response);
    }
}