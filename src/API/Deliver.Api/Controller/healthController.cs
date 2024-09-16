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
        public ActionResult CheckServerHealth()
        {
            return Ok();
        }
    }
}
