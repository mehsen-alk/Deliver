using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Trips.Commands.RiderCreateTrip;
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<RiderCreateTripDto>> CreateTrip(
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
}