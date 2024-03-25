
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _memoryCache;

        public ChatHub(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public override async Task OnConnectedAsync()
        {
            await RetrieveMessagesFromCache();
            await base.OnConnectedAsync();
        }
        private async Task RetrieveMessagesFromCache()
        {
            if (_memoryCache.TryGetValue("messages", out List<string> messages))
            {
                foreach (var message in messages)
                {
                    var messageObj = JsonConvert.DeserializeObject<Messages>(message);
                    await Clients.Caller.SendAsync("ReceiveMessage", messageObj.User, messageObj.Message);
                }
            }
        }
        public async Task SendMessage(string user, string message)
        {
            var messageObj = new Messages { User = user, Message = message };

            if (!_memoryCache.TryGetValue("messages", out List<string> messages))
            {
                messages = new List<string>();
            }

            messages.Add(JsonConvert.SerializeObject(messageObj));

            if (messages.Count > 10)
            {
                messages.RemoveAt(0);
            }

            _memoryCache.Set("messages", messages);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public class Messages
        {
            public string User { get; set; }
            public string Message { get; set; }
        }
    }
}
