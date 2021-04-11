using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Cria.Models;
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

        public IEnumerable<T> GetAllItems<T>()
        {
            return Storage.Keys.Select(GetItem<T>).ToList();
        }

        public T GetItem<T>(string filename)
        {
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
        IEnumerable<T> GetAllItems<T>();
    }
}