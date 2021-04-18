using System;

namespace Cria.Models
{
    public class DrawEntryResponse
    {
        public Guid TicketId { get; }

        public DrawEntryResponse(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}