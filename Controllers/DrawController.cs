using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Cria.Models;
using Cria.Services;
using Microsoft.AspNetCore.Http;

namespace Cria.Controllers
{
    [ApiController]
    public class DrawController : ControllerBase
    {
        private const string RouteBase = "api/draw";

        private readonly ILogger<DrawController> _logger;
        private readonly IDrawService _drawService;

        public DrawController(ILogger<DrawController> logger, IDrawService drawService)
        {
            _logger = logger;
            _drawService = drawService;
        }

        [HttpPost]
        [Route(RouteBase)]
        public IActionResult DoDraw([FromBody] DrawRequest request)
        {
            var drawResult = _drawService.DoDraw(request.MaxWinners);

            var winners = drawResult.WinningTickets.Select(ticket => new WinnerResponse {Name = ticket.Name, ticketId = ticket.Id}).ToList();

            var responseBody = new DrawResponse(drawResult.DrawId, winners);

            Response.StatusCode = StatusCodes.Status200OK;

            return new JsonResult(responseBody);
        }

        [HttpGet]
        [Route(RouteBase + "/{drawId}")]
        public IActionResult GetDrawById(string id)
        {
            if (!Guid.TryParse(id, out var drawId))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new EmptyResult();
            }

            var drawResult = _drawService.GetDrawById(drawId);

            if (drawResult == default)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new EmptyResult();
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(drawResult);
        }

        [HttpGet]
        [Route(RouteBase)]
        public IActionResult GetAllDrawIds()
        {
            var allDrawIds = _drawService.GetAllDrawIds();

            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(allDrawIds);
        }
    }

    public class WinnerResponse
    {
        public Guid ticketId { get; set; }
        public string Name { get; set; }
    }
}
