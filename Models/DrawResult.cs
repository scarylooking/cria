using System;
using System.Collections.Generic;
using System.Linq;

namespace Cria.Models
{
    public class DrawResult
    {
        public Guid DrawId { get; }
        public DateTimeOffset ConductedAtUtc { get; }
        public List<Ticket> WinningTickets { get; }

        public DrawResult(IEnumerable<Ticket> winningTickets)
        {
            DrawId = Guid.NewGuid();
            ConductedAtUtc = DateTimeOffset.UtcNow;
            WinningTickets = winningTickets.ToList();
        }
    }
}