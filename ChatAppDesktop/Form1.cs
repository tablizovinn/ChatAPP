using System;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebGrease;

namespace ChatAppDesktop
{
    public partial class Form1 : Form
    {
        HubConnection hubConnection;
        string signalRConnection = ConfigurationManager.AppSettings["hubConnection"];


        public Form1()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder().WithUrl(signalRConnection).Build();


            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {

                messageRTB.Invoke((MethodInvoker)delegate
                {
                    messageRTB.AppendText($"{user}: {message}\n");
                });
            });


            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(0);
                MessageBox.Show("SignalR connection closed.");
                if (error != null)
                {
                    MessageBox.Show($"Connection closed due to an error: {error.Message}");
                }
            };


            hubConnection.StartAsync().ContinueWith(task =>
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
        }

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                string user = usernameTB.Text;
                string message = messageTB.Text;


                await hubConnection.SendAsync("SendMessage", user, message);


                messageTB.Clear();
            }
            else
            {
                MessageBox.Show("SignalR connection is not active.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }



        private async void cacheBtn_Click_1(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    
                    Task<IEnumerable<(string, string)>> cachedItemsTask =
                        hubConnection.InvokeAsync<IEnumerable<(string, string)>>("GetAllCachedItems");

                  
                    IEnumerable<(string, string)> cachedItems = await cachedItemsTask;

                    if (cachedItems != null)
                    {
                        foreach (var item in cachedItems)
                        {
                         
                            MessageBox.Show($"User: {item.Item1}, Message: {item.Item2}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No cached items available.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("SignalR connection is not active.");
            }
        }
    }
}