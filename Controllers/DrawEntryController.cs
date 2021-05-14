using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Cria.Models;
using Cria.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Cria.Controllers
{
    [ApiController]
    
    public class DrawEntryController : ControllerBase
    {
        private const string RouteBase = "api/drawEntry";

        private readonly ILogger<DrawEntryController> _logger;
        private readonly ITicketService _ticketService;
        private readonly ICaptchaService _captchaService;

        public DrawEntryController(ILogger<DrawEntryController> logger, ITicketService ticketService, ICaptchaService captchaService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _captchaService = captchaService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(RouteBase)]
        public async Task <IActionResult> CreateEntry([FromBody] DrawEntryRequest request)
        {
            var ticketId =_ticketService.CreateTicketForRequest(request);

            if (!await _captchaService.IsValid(request.ReCaptchaToken))
            {
                return BadRequest();
            }

            var responseBody = new DrawEntryResponse(ticketId);

            Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(responseBody);
        }
    }
}
