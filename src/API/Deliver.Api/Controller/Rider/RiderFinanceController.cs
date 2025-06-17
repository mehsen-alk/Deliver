using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Finance.Rider.Query.GetRiderPayment;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Rider;

[ApiController]
[Route("v1/rider/finance")]
[Authorize(Roles = "Rider")]
public class RiderFinanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public RiderFinanceController(
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
        _userContextService = userContextService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BaseResponse<RiderPaymentsVm>>> GetRiderPayment()
    {
        var userId = _userContextService.GetUserId();

        var request = new GetRiderPaymentQuery { UserId = userId };

        var data = await _mediator.Send(request);

        var response = BaseResponse<RiderPaymentsVm>.FetchedSuccessfully(data: data);

        return Ok(response);
    }
}