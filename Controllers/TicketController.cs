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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Cria.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public IActionResult GetTicket(Guid ticketId)
        {

            var ticket = _ticketService.GetTicket(ticketId);

            if (ticket == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new EmptyResult();
            }

            Response.StatusCode = StatusCodes.Status200OK;

            return new JsonResult(ticket);
        }

        [HttpGet("all")]
        public IActionResult GetAllTickets()
        {
            var tickets = _ticketService.GetAllTickets();

            Response.StatusCode = StatusCodes.Status200OK;

            return new JsonResult(tickets);
        }
    }
}
