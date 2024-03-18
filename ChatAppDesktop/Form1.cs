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
        List<string> messageKeys = new List<string>(); 

        public Form1()
        {
            InitializeComponent();
            hub_connection = new HubConnectionBuilder().WithUrl(signalRconnection).Build();

            
            hub_connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                
                CacheAndDisplayMessage(user, message);
            });

           
            hub_connection.Closed += async (error) =>
            {
                await Task.Delay(0);
                MessageBox.Show("SignalR connection closed.");
                if (error != null)
                {
                    MessageBox.Show($"Connection closed due to an error: {error.Message}");
                }
            };

            
            hub_connection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show($"SignalR connection failed to start: {task.Exception.Message}");
                }
                else
                {
                   
                    sendBtn.Enabled = true;
                }
            });

        
            DisplayCachedMessages();
        }

        private void CacheAndDisplayMessage(string user, string message)
        {
            
            string key = $"{user}_{DateTime.Now.Ticks}";
            messageCache.Set(key, $"{user}: {message}", TimeSpan.FromMinutes(10)); 
            messageKeys.Add(key);

          
            UpdateMessageBox(user, message);
        }

        private void DisplayCachedMessages()
        {
            
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
           
            messageRTB.Invoke((MethodInvoker)delegate
            {
                messageRTB.AppendText($"{user}: {message}\n");
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            if (hub_connection.State == HubConnectionState.Connected)
            {
                string user = usernameTB.Text;
                string message = messageTB.Text;

                
                await hub_connection.SendAsync("SendMessage", user, message);

          
                messageTB.Clear();
            }
            else
            {
                MessageBox.Show("SignalR connection is not active.");
            }
        }
    }
}
