using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cria.Controllers.Models;
using Cria.Models;
using Cria.Services;
using Microsoft.AspNetCore.Http;

namespace Cria.Controllers
{
    [ApiController]
    [Route("api/drawEntry")]
    public class DrawEntryController : ControllerBase
    {
        private readonly ILogger<DrawEntryController> _logger;
        private readonly ITicketService _ticketService;

        public DrawEntryController(ILogger<DrawEntryController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        [HttpPost]
        public IActionResult CreateEntry([FromBody] DrawEntryRequest request)
        {
            var ticketId =_ticketService.CreateTicketForRequest(request);

            var responseBody = new DrawEntryResponse(ticketId);

            Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(responseBody);
        }
    }
}
