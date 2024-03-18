using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatAppDesktop
{
    public partial class Form1 : Form
    {
        HubConnection hub_connection;
        public Form1()
        {
            InitializeComponent();
            hub_connection = new HubConnectionBuilder().WithUrl("https://localhost:7296/chathub").Build();

            hub_connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                messageRTB.Invoke((MethodInvoker)delegate
                {
                    messageRTB.AppendText($"{user}: {message}\n");
                });
            });

            // Subscribe to Closed event
            hub_connection.Closed += async (error) =>
            {
                // Connection is closed
                // Handle the error if there is one
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
                   
                    // Enable the send button after the connection is established
                    sendBtn.Enabled = true;
                }
            });

        }

        private void  Form1_Load(object sender, EventArgs e)
        {
           
        }

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            if(hub_connection.State == HubConnectionState.Connected)
            {
                string user = usernameTB.Text;
                string message = messageTB.Text;

                await hub_connection.SendAsync("SendMessage",user, message);

                messageTB.Clear();
            }
            else
            {
                MessageBox.Show("SignalR connection is not active.");
            }
        }
    }
}
