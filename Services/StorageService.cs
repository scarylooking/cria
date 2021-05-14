using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Cria.Services
{
    public class StorageService : IStorageService
    {
        private readonly ILogger<TicketService> _logger;

        private static readonly Dictionary<string,string> Storage = new();

        public StorageService(ILogger<TicketService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<T> GetAllItems<T>(string itemType)
        {
            return Storage.Keys
                .Where(key => key.StartsWith($"{itemType}_", StringComparison.CurrentCultureIgnoreCase))
                .Select(GetItem<T>).ToList();
        }

        public IEnumerable<string> GetAllItemNamesForType(string itemType)
        {
            return Storage.Keys
                .Where(key => key.StartsWith($"{itemType}_", StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public T GetItem<T>(string filename)
        {
            if (!Storage.ContainsKey(filename))
            {
                _logger.LogWarning($"a request was made for ${filename} from storage but no item with that name exists");
                return default;
            }

            var itemString = Storage[filename];
            return JsonSerializer.Deserialize<T>(itemString);
        }

        public void RemoveItem(string filename)
        {
            Storage.Remove(filename);
        }

        public void StoreItem<T>(string filename, T item)
        {
            Storage.Add(filename, JsonSerializer.Serialize(item));
        }

    }

    public interface IStorageService
    {
        void StoreItem<T>(string filename, T item);
        T GetItem<T>(string filename);
        void RemoveItem(string filename);
        IEnumerable<T> GetAllItems<T>(string itemType);
        IEnumerable<string> GetAllItemNamesForType(string itemType);
    }
}