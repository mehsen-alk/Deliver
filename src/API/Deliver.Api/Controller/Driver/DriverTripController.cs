using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;
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

    public DriverTripController(
        IMapper mapper,
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
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
}