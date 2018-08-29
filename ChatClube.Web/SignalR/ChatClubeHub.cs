using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChatClube.Web.SignalR
{
    public class ChatClubeHub : Hub
    {

        public void Enviar(string nome, string mensagem)
        {
            Clients.All.broadcastMessage(nome, mensagem);

        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}