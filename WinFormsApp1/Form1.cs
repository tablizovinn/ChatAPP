using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.Caching.Memory;

namespace ChatAppWinForms
{
    public partial class Form1 : Form
    {
        private readonly MemoryCache _memoryCache;
        private readonly YourCacheManager _cacheManager;

        public Form1()
        {
            InitializeComponent();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheManager = new YourCacheManager(_memoryCache);
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string user = usernameTB.Text;
            string message = messageTB.Text;

            _cacheManager.AddToCache(user, message);
            MessageBox.Show("Item cached successfully.");
        }

        private void displayBtn_Click(object sender, EventArgs e)
        {
            messageRTB.Clear();
            foreach (var item in _cacheManager.GetAllCachedItems())
            {
                messageRTB.AppendText($"User: {item.User}, Message: {item.Message}\n");
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
}
