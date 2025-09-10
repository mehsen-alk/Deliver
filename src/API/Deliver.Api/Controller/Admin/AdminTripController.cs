using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Trips.AdminTrips.Query.FilterTrips;
using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Admin;

[ApiController]
[Route("v1/admin/trip")]
[Authorize(Roles = "Admin")]
public class AdminTripController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public AdminTripController(IMediator mediator, IUserContextService userContextService)
    {
        _mediator = mediator;
        _userContextService = userContextService;
    }

    /// <response code="404">There is no Current Trip</response>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponse<List<TripHistoryVm>>>> FilterTrip(
        [FromQuery] FilterTripForAdminQuery query
    )
    {
        var data = await _mediator.Send(query);

        var response = BaseResponse<List<TripHistoryVm>>.FetchedSuccessfully(data: data);

        return Ok(response);
    }
}