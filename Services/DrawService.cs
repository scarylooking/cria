using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text.RegularExpressions;
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

        private readonly Regex _drawIdRegex;

        public DrawService(ILogger<TicketService> logger, IStorageService storageService, ITicketService ticketService)
        {
            _logger = logger;
            _storageService = storageService;
            _ticketService = ticketService;

            _drawIdRegex = new Regex(@"draw-result_([a-f,0-9,-]*)\.json");
        }

        public DrawResult DoDraw(int maxWinners)
        {
            var validEntries = GetValidEntries();

            var winningTicketIds = DrawWinningTickets(validEntries, maxWinners);

            var winningTickets = winningTicketIds.Select(winningTicketId => _ticketService.GetTicket(winningTicketId)).ToList();

            var result = new DrawResult(winningTickets);

            StoreResult(result);

            return result;
        }

        public DrawResult GetDrawById(Guid drawId)
        {
            return _storageService.GetItem<DrawResult>(GenerateFilenameFromDrawResultId(drawId));
        }

        public IEnumerable<Guid> GetAllDrawIds()
        {
            var allDraws = _storageService.GetAllItemNamesForType("draw-result");

            var drawIds = new List<Guid>();

            foreach (var draw in allDraws)
            {
                var match = _drawIdRegex.Match(draw);

                if (match.Success)
                {
                    var drawId = Guid.Parse(match.Groups[1].Value);

                    drawIds.Add(drawId);
                }
            }

            return drawIds;
        }

        private void StoreResult(DrawResult result)
        {
            _storageService.StoreItem(GenerateFilenameFromDrawResultId(result.DrawId), result);
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

            var winners = new List<Guid>(entryIds);

            for (var i = winners.Count - 1; i > 0; i--)
            {
                var randomIndex = random.Next(0, i + 1);
                var temp = winners[i];
                winners[i] = winners[randomIndex];
                winners[randomIndex] = temp;
            }

            return winners.Take(Math.Min(winners.Count, max)).ToArray();
        }

        private string GenerateFilenameFromDrawResultId(Guid drawResultId)
        {
            return $"draw-result_{drawResultId}.json";
        }
    }

    public interface IDrawService
    {
        DrawResult DoDraw(int maxWinners);

        DrawResult GetDrawById(Guid drawId);

        IEnumerable<Guid> GetAllDrawIds();
    }
}