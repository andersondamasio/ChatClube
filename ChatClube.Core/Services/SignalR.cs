using com.chatclube.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.chatclube.Services
{
    public class SignalR
    {

        private static string baseURL => Geral.IsDebug() ? "http://192.168.1.6:51042/ChatClubeHub" : "https://chatclube.azurewebsites.net/ChatClubeHub";

        private static HubConnection hubConnectionP;
        public async static Task<HubConnection> GetHubConnection()
        {
            
                if (hubConnectionP == null)
                    hubConnectionP = await GetConnection();

            hubConnectionP.Closed -= Conn_Closed;
            hubConnectionP.Closed += Conn_Closed;

            return hubConnectionP;
        }

        public async static Task<HubConnection> GetConnection()
        {
            HubConnection conn = null;
            try
            {
                conn = new HubConnectionBuilder()
                                .WithUrl(baseURL)
                                .Build();


                await conn.StartAsync();
            }
            catch (Exception ex)
            {
                var teste = ex;
            }
            return conn;
        }

        private static Task Conn_Closed(Exception arg)
        {
            return GetHubConnection();
        }
    }
}
