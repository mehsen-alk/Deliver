using AutoMapper;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Distance.Query.GetDistance;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller;

[Route("v1/distance")]
[ApiController]
[Authorize]
public class DistanceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public DistanceController(
        IMapper mapper,
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mapper = mapper;
        _mediator = mediator;
        _userContextService = userContextService;
    }

    /// <summary>
    ///     calculate the distance in KM and duration in minute for two geo points.
    /// </summary>
    [HttpGet("distance")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse<DistanceVm>>> GetDistance(
        [FromQuery] GetDistanceQuery query
    )
    {
        var vm = await _mediator.Send(query);

        return Ok(BaseResponse<DistanceVm>.FetchedSuccessfully(data: vm));
    }
}