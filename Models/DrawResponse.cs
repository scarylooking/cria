using System;
using System.Collections.Generic;
using System.Linq;
using Cria.Controllers;

namespace Cria.Models
{
    public class DrawResponse
    {
        public Guid DrawId { get; }
        public DateTimeOffset ConductedAtUTC { get; }
        public List<WinnerResponse> WinningTickets { get; }

        public DrawResponse(Guid drawId, IEnumerable<WinnerResponse> winners)
        {
            DrawId = drawId;
            ConductedAtUTC = DateTimeOffset.UtcNow;
            WinningTickets = winners.ToList();
        }
    }
}