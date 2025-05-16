using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiteApi.Controllers
{
    [ApiController]
    [Authorize]
    public class AdmController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");

    }
}
