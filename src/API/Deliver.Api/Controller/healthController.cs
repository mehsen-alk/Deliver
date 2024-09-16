using Deliver.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        public HealthController()
        {
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public ActionResult CheckServerHealth()
        {
            return Ok(BaseResponse.FetchSuccessfully());
        }
    }
}
