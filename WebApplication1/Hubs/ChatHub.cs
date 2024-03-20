using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        public ChatHub(IMemoryCache cache)
        {
            _memoryCache = cache;
        }

        public async Task SendMessage(string user, string message)
        {
            string cacheKey = $"{user}_{message}";
            string cachedMessage = _memoryCache.Get<string>(cacheKey);

            if (cachedMessage == null)
            {
                cachedMessage = message;
                _memoryCache.Set(cacheKey, cachedMessage, TimeSpan.FromSeconds(30));
            }

            await Clients.All.SendAsync("ReceiveMessage", user, cachedMessage);
        }

        public string GetCacheMessage(string user, string message)
        {
            string cacheKey = $"{user}_{message}";
            string cachedMessage = _memoryCache.Get<string>(cacheKey);

            return cachedMessage;
        }
    }
}
