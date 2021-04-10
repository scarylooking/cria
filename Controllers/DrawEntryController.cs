using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cria.Controllers.Models;

namespace Cria.Controllers
{
    [ApiController]
    [Route("api/drawEntry")]
    public class DrawEntryController : ControllerBase
    {
        private readonly ILogger<DrawEntryController> _logger;

        public DrawEntryController(ILogger<DrawEntryController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public HttpResponseMessage CreateEntry([FromBody] DrawEntryRequest whatever)
        {
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
