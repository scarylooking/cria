using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cria.Models;
using Microsoft.Extensions.Logging;

namespace Cria.Services
{
    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> _logger;
        private readonly IStorageService _storage;

        public TicketService(ILogger<TicketService> logger, IStorageService storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public Ticket GetTicket(Guid ticketId)
        {
            return _storage.GetItem<Ticket>(GenerateFilenameFromTicketId(ticketId));
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            foreach (var ticket in _storage.GetAllItems<Ticket>("ticket"))
            {
                yield return ticket;
            }
        }

        public Guid CreateTicketForRequest(DrawEntryRequest request)
        {
            var ticket = new Ticket
            {
                Email = request.Email.Trim().ToLower(),
                Name = request.Name,
                Prize = request.Prize.Trim().ToLower()
            };

            _storage.StoreItem(GenerateFilenameFromTicketId(ticket.Id), ticket);

            return ticket.Id;
        }

        public void DeleteTicket(Guid ticketId)
        {
            _storage.RemoveItem(GenerateFilenameFromTicketId(ticketId));
        }

        private string GenerateFilenameFromTicketId(Guid ticketId)
        {
            return $"ticket_{ticketId}.json";
        }
    }

    public interface ITicketService
    {
        Guid CreateTicketForRequest(DrawEntryRequest request);
        void DeleteTicket(Guid entryId);
        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicket(Guid ticketId);
    }
}