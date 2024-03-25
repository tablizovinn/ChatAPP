using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ChatAppDesktop
{
    public partial class Form1 : Form
    {
        private HubConnection hubConnection;
        private readonly string signalRConnection = ConfigurationManager.AppSettings["hubConnection"];

        public Form1()
        {
            InitializeComponent();

            hubConnection = new HubConnectionBuilder()
                .WithUrl(signalRConnection)
                .Build();

          
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                DisplayMessage($"{user}: {message}");
            });

     
            StartHubConnection();
        }

        private async void StartHubConnection()
        {
            
                await hubConnection.StartAsync();
                MessageBox.Show("Connected to SignalR hub.");

                
                await RetrieveMessagesFromCache();
            
            
            
        }

        private async Task RetrieveMessagesFromCache()
        {
            
              
                await hubConnection.InvokeAsync("OnConnectedAsync");
           
            
        }

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                string user = usernameTB.Text;
                string message = messageTB.Text;

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(message))
                {
                    try
                    {
                       
                        await hubConnection.SendAsync("SendMessage", user, message);
                        messageTB.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error sending message: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a user name and message.");
                }
            }
            else
            {
                MessageBox.Show("SignalR connection is not active.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void DisplayMessage(string message)
{
       if (MessageListBox.InvokeRequired)
    {
      
        MessageListBox.Invoke((MethodInvoker)(() => DisplayMessage(message)));
    }
    else
    {
        
        MessageListBox.Items.Add(message);
    }
}

    }
}
