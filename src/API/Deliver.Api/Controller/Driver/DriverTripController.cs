using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Trips.Common.AddressRequest;
using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using Deliver.Application.Features.Trips.DriverTrips.Commands.DriverAcceptTrip;
using Deliver.Application.Features.Trips.DriverTrips.Commands.DriverCancelTrip;
using
    Deliver.Application.Features.Trips.DriverTrips.Commands.DriverUpdateTripStatusToNext;
using Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverAvailableTrips;
using Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverCurrentTrip;
using Deliver.Application.Features.Trips.DriverTrips.Query.GetTripHistory;
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

            return Ok(notFoundResponse);
        }

        var response = BaseResponse<DriverCurrentTripVm>.FetchedSuccessfully(data: data);

        return Ok(response);
    }

    [HttpPut("accept")]
    [ProducesResponseType(
        typeof(BaseResponse<DriverAcceptTripVm>),
        StatusCodes.Status202Accepted
    )]
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

        return Ok(response);
    }

    /// <response code="1005">You Don't Have An Active Trip</response>
    /// <response code="1006">You have exceeded the time allowed to cancel the trip.</response>
    [HttpDelete]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(1005)]
    [ProducesResponseType(1006)]
    public async Task<ActionResult<BaseResponse<DriverAcceptTripVm>>> CancelTrip()
    {
        var driverId = _userContextService.GetUserId();

        var command = new DriverCancelTripCommand { UserId = driverId };

        var data = await _mediator.Send(command);

        var response = BaseResponse<string>.DeletedSuccessfully(data: data.ToString());

        return Ok(response);
    }

    [HttpGet("history")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse<List<TripHistoryVm>>>> GetTripHistory(
        [FromQuery] GetDriverTripHistoryRequest request
    )
    {
        var userId = _userContextService.GetUserId();

        var query = new GetDriverTripHistoryQuery
        {
            DriverId = userId,
            Page = request.Page
        };

        var result = await _mediator.Send(query);

        return Ok(BaseResponse<List<TripHistoryVm>>.FetchedSuccessfully(data: result));
    }

    /// <response code="1005">You Don't Have An Active Trip</response>
    /// <response code="1007">Trip Status Cant Be Updated To Next Status</response>
    [HttpPut("next")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(1005)]
    [ProducesResponseType(1007)]
    public async Task<ActionResult<BaseResponse<DriverAcceptTripVm>>> UpdateToNextStatus(
        [FromBody] AddressRequest driverAddress
    )
    {
        var query = new DriverUpdateTripStatusToNextCommand
        {
            DriverId = _userContextService.GetUserId(),
            DriverAddress = driverAddress
        };

        var data = await _mediator.Send(query);

        var response = BaseResponse<string>.UpdatedSuccessfully(data: data.ToString());

        return Ok(response);
    }
}