using ChatClube.Web.Data.Config;
using com.chatclube.Data.Repository.UsuarioX;
using com.chatclube.Models;
using com.chatclube.Repository.Config;
using com.chatclube.UsuarioX;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace chatclube.com.Hubs
{
    public class ChatClubeHub : Hub
    {
        public ChatClubeHub(DBContextCoreSQLServer DBContextCoreSQLServer)
        {
            DBContextCore.DbType = DBContextCoreSQLServer;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task EnviarMensagem(Mensagem chatMessage)
        {
            await Clients.All.SendAsync("ReceberMensagem", chatMessage);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task Conectar(Usuario usuario)
        {
            string user = Context.User.Identity.Name;

            if (usuario != null)
              await new UsuarioRepository().SalvarUsuarioAsync(usuario);
        }
    }
}
