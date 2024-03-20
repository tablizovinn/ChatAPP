using Microsoft.Extensions.Caching.Memory;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly IMemoryCache _memoryCache;

        public Form1()
        {
            InitializeComponent();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string key = user.Text.Trim();
            string value = message.Text.Trim();

            // Add or update the cache entry
            _memoryCache.Set(key, value, TimeSpan.FromSeconds(10));

            MessageBox.Show("Value saved to cache successfully!");
        }

        private void btnRet_Click(object sender, EventArgs e)
        {
            // Get the key from the textbox
            string key = user.Text.Trim();

            // Try to retrieve the value from the cache
            if (_memoryCache.TryGetValue(key, out string cachedValue))
            {
                // Display the cached value
                display.AppendText($"Cached value for key '{key}': {cachedValue}");
            }
            else
            {
                MessageBox.Show($"No value found in cache for key '{key}'");
            }
        }
    }
}
