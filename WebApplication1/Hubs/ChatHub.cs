using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<string, string> _cachedKeys;

        public ChatHub(IMemoryCache cache)
        {
            _memoryCache = cache;
            _cachedKeys = new Dictionary<string, string>();
        }

        public async Task SendMessage(string user, string message)
        {
            string key = GenerateCacheKey(user, message);
            string cachedMessage = _memoryCache.Get<string>(key);

            if (cachedMessage == null)
            {
                cachedMessage = message;
                _memoryCache.Set(key, cachedMessage);
                _cachedKeys[key] = $"{user}_{message}";
            }

            await Clients.All.SendAsync("ReceiveMessage", user, cachedMessage);
        }


        public Task<IEnumerable<(string, string)>> GetAllCachedItems()
        {
            var cachedItems = new List<(string, string)>();

            foreach (var kvp in _cachedKeys)
            {
                var userMessage = kvp.Value.Split('_');
                var user = userMessage[0];
                var message = userMessage[1];
                cachedItems.Add((user, message));
            }

            return Task.FromResult<IEnumerable<(string, string)>>(cachedItems);
        }


        private string GenerateCacheKey(string user, string message)
        {
            return $"{user}_{message}";
        }
    }
}
