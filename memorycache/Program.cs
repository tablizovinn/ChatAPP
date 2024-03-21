using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

public class Program
{
    public static void Main(string[] args)
    {
        
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
            
        
        var cacheManager = new YourCacheManager(memoryCache);

        
        while (true)
        {
            Console.WriteLine("Enter user (or type 'exit' to quit):");
            string user = Console.ReadLine();

            if (user.ToLower() == "exit")
                break;

            Console.WriteLine("Enter message:");
            string message = Console.ReadLine();

            
            cacheManager.AddToCache(user, message);

            Console.WriteLine("Item cached successfully.");
        }

       
        Console.WriteLine("Cached items:");
        foreach (var item in cacheManager.GetAllCachedItems())
        {
            Console.WriteLine($"User: {item.User}, Message: {item.Message}");
        }
    }
}

public class YourCacheManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly Dictionary<string, string> _cachedKeys;

    public YourCacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _cachedKeys = new Dictionary<string, string>();
    }

    public void AddToCache(string user, string message)
    {
        string key = GenerateCacheKey(user, message);
        _memoryCache.Set(key, true); 
        _cachedKeys[key] = $"{user}_{message}";
    }

    
    [("GetAllCachedItems")]
    public IEnumerable<(string User, string Message)> GetAllCachedItems()
    {
        foreach (var kvp in _cachedKeys)
        {
            var userMessage = kvp.Value.Split('_');
            var user = userMessage[0];
            var message = userMessage[1];
            yield return (user, message);
        }
    }

    private string GenerateCacheKey(string user, string message)
    {
        return $"{user}_{message}";
    }

  

}
