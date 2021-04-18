using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cria.Models;
using Cria.Services;
using Microsoft.AspNetCore.Http;

namespace Cria.Controllers
{
    [ApiController]
    [Route("api/draw")]
    public class DrawController : ControllerBase
    {
        private readonly ILogger<DrawController> _logger;
        private readonly IDrawService _drawService;

        public DrawController(ILogger<DrawController> logger, IDrawService drawService)
        {
            _logger = logger;
            _drawService = drawService;
        }

        [HttpPost]
        public IActionResult DoDraw([FromBody] DrawRequest request)
        {
            var drawResult = _drawService.DoDraw(request.MaxWinners);

            var winners = drawResult.WinningTickets.Select(ticket => new WinnerResponse {Name = ticket.Name, ticketId = ticket.Id}).ToList();

            var responseBody = new DrawResponse(drawResult.DrawId, winners);

            Response.StatusCode = StatusCodes.Status200OK;

            return new JsonResult(responseBody);
        }
    }

    public class WinnerResponse
    {
        public Guid ticketId { get; set; }
        public string Name { get; set; }
    }
}
