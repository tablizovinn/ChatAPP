using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ChatAppConsole
{
    class Program
    {
        static MemoryCache messageCache = new MemoryCache("MessageCache");

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Chat App Console!");

            // Simulate receiving messages
            ReceiveMessage("Alice", "Hello, everyone!");
            ReceiveMessage("Bob", "Hi, Alice!");

            // Display cached messages
            LoadCachedMessages();

            // Simulate sending a message
            SendMessage("Alice", "How's everyone doing?");

            // Display cached messages again
            LoadCachedMessages();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void ReceiveMessage(string user, string message)
        {
            // Cache the received message
            CacheMessage(user, message);
            Console.WriteLine($"Received message from {user}: {message}");
        }

        static void SendMessage(string user, string message)
        {
            // Simulate sending a message
            Console.WriteLine($"Sending message from {user}: {message}");
        }

        static void CacheMessage(string user, string message)
        {
            // Construct a unique key for the message based on user and timestamp
            string key = $"{user}_{DateTime.Now.Ticks}";

            // Cache the message with a 10-minute expiration
            messageCache.Set(key, message, DateTimeOffset.Now.AddMinutes(1));
        }

        static void LoadCachedMessages()
        {
            Console.WriteLine("Cached Messages:");
            foreach (var cacheItem in messageCache)
            {
                var message = cacheItem.Value as string;
                Console.WriteLine($"{cacheItem.Key}: {message}");
            }
            Console.WriteLine();
        }
    }
}
