using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace chatclube.com.Hubs
{
    public class ChatClubeHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
