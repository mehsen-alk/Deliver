using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Finance.Driver.Query.GetDriverProfits;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Driver;

[ApiController]
[Route("v1/driver/finance")]
[Authorize(Roles = "Driver")]
public class DriverFinanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public DriverFinanceController(
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
        _userContextService = userContextService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse<DriverEarningsVm>>> GetDriverFinance()
    {
        var userId = _userContextService.GetUserId();

        var request = new GetDriverProfitsQuery { UserId = userId };

        var data = await _mediator.Send(request);

        var response = BaseResponse<DriverEarningsVm>.FetchedSuccessfully(data: data);

        return Ok(response);
    }
}