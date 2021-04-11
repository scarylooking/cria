using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Cria.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;

namespace Cria.Services
{
    public class DrawService : IDrawService
    {
        private readonly ILogger<TicketService> _logger;
        private readonly IStorageService _storageService;
        private readonly ITicketService _ticketService;

        public DrawService(ILogger<TicketService> logger, IStorageService storageService, ITicketService ticketService)
        {
            _logger = logger;
            _storageService = storageService;
            _ticketService = ticketService;
        }

        public IReadOnlyCollection<Guid> DoDraw()
        {
            var validEntries = GetValidEntries();

            return DrawWinningTickets(validEntries);
        }

        private IReadOnlyCollection<Guid> GetValidEntries()
        {
            var emailAddresses = new HashSet<string>();
            var validEntries = new List<Guid>();

            foreach (var entry in _ticketService.GetAllTickets())
            {
                var entryEmailAddress = entry.Email.Trim().ToLower();

                if (!emailAddresses.Contains(entryEmailAddress))
                {
                    emailAddresses.Add(entryEmailAddress);
                    validEntries.Add(entry.Id);
                }
            }

            return validEntries;
        }

        private IReadOnlyList<Guid> DrawWinningTickets(IEnumerable<Guid> entryIds, int max = int.MaxValue)
        {
            var random = new Random();

            var winningEntryIds = new List<Guid>(entryIds);

            for (var i = Math.Min(winningEntryIds.Count, max) - 1; i > 0; i--)
            {
                var randomIndex = random.Next(0, i + 1);
                var temp = winningEntryIds[i];
                winningEntryIds[i] = winningEntryIds[randomIndex];
                winningEntryIds[randomIndex] = temp;
            }

            return winningEntryIds;
        }

    }

    public interface IDrawService
    {
        IReadOnlyCollection<Guid> DoDraw();
    }
}