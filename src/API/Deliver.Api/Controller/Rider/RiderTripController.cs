using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Trips.Commands.RiderCancelTrip;
using Deliver.Application.Features.Trips.Commands.RiderCreateTrip;
using Deliver.Application.Features.Trips.Query.GetRiderCurrentTrip;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Rider;

[Route("v1/rider/trip")]
[ApiController]
[Authorize(Roles = "Rider")]
public class RiderTripController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public RiderTripController(
        IMediator mediator,
        IMapper mapper,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
        _mapper = mapper;
        _userContextService = userContextService;
    }

    /// <response code="1001">Current Trip Exist</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(1001)]
    public async Task<ActionResult<BaseResponse<RiderCreateTripDto>>> CreateTrip(
        [FromBody] RiderCreateTripRequest request
    )
    {
        var command = _mapper.Map<RiderCreateTripCommand>(request);

        command.RiderId = _userContextService.GetUserId();

        var data = await _mediator.Send(command);

        var response = BaseResponse<RiderCreateTripDto>.CreatedSuccessfully(
            data: data,
            message: "Trip created."
        );

        return CreatedAtAction(nameof(CreateTrip), response);
    }

    /// <response code="404">There is no Current Trip</response>
    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponse<RiderCurrentTripVm>>> GetCurrentTrip()
    {
        var query =
            new GetRiderCurrentTripQuery { RiderId = _userContextService.GetUserId() };

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

        var response = BaseResponse<RiderCurrentTripVm>.FetchedSuccessfully(data: data);

        return Ok(response);
    }

    /// <response code="1005">user did not have active trip</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(1005)]
    public async Task<ActionResult<BaseResponse<string>>> CancelTrip()
    {
        var command =
            new RiderCancelTripCommand { UserId = _userContextService.GetUserId() };

        var result = await _mediator.Send(command);

        return Accepted(
            BaseResponse<string>.DeletedSuccessfully(data: result.ToString())
        );
    }
}