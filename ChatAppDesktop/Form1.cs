using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ChatAppDesktop
{
    public partial class Form1 : Form
    {
        HubConnection hub_connection;
        string signalRconnection = ConfigurationManager.AppSettings["hubConnection"];
        IMemoryCache messageCache = new MemoryCache(new MemoryCacheOptions());
        List<string> messageKeys = new List<string>(); // Maintain a list of cached message keys

        public Form1()
        {
            InitializeComponent();
            hub_connection = new HubConnectionBuilder().WithUrl(signalRconnection).Build();

            // Subscribe to the "ReceiveMessage" event
            hub_connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // Cache and display the received message
                CacheAndDisplayMessage(user, message);
            });

            // Subscribe to the "Closed" event
            hub_connection.Closed += async (error) =>
            {
                await Task.Delay(0);
                MessageBox.Show("SignalR connection closed.");
                if (error != null)
                {
                    MessageBox.Show($"Connection closed due to an error: {error.Message}");
                }
            };

            // Start the SignalR connection
            hub_connection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show($"SignalR connection failed to start: {task.Exception.Message}");
                }
                else
                {
                    // Enable the send button after the connection is established
                    sendBtn.Enabled = true;
                }
            });

            // Display cached messages
            DisplayCachedMessages();
        }

        private void CacheAndDisplayMessage(string user, string message)
        {
            // Cache the message with a unique key
            string key = $"{user}_{DateTime.Now.Ticks}";
            messageCache.Set(key, $"{user}: {message}", TimeSpan.FromMinutes(10)); // Cache for 10 minutes
            messageKeys.Add(key); // Add the key to the list

            // Update the message box
            UpdateMessageBox(user, message);
        }

        private void DisplayCachedMessages()
        {
            // Iterate over the list of message keys and retrieve the corresponding values
            foreach (var key in messageKeys)
            {
                if (messageCache.TryGetValue(key, out object value))
                {
                    messageRTB.AppendText(value.ToString() + "\n");
                }
            }
        }

        private void UpdateMessageBox(string user, string message)
        {
            // Update the message box with the new message
            messageRTB.Invoke((MethodInvoker)delegate
            {
                messageRTB.AppendText($"{user}: {message}\n");
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form load event
        }

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            if (hub_connection.State == HubConnectionState.Connected)
            {
                string user = usernameTB.Text;
                string message = messageTB.Text;

                // Send the message via SignalR
                await hub_connection.SendAsync("SendMessage", user, message);

                // Clear the message text box
                messageTB.Clear();
            }
            else
            {
                MessageBox.Show("SignalR connection is not active.");
            }
        }
    }
}
