using com.chatclube.Data.Repository.UsuarioX;
using com.chatclube.UsuarioX;
using com.chatclube.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using Polly;
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
            try
            {
                var pauseBetweenFailures = TimeSpan.FromSeconds(20);

                var retryPolicy = Policy.Handle<Exception>().WaitAndRetryForeverAsync(i => pauseBetweenFailures, (exception, timeSpan) =>
             {});

                await retryPolicy.ExecuteAsync(async () =>
                {
                    await GetConnection();
                });

                if (hubConnectionP == null)
                    hubConnectionP = await GetConnection();

                hubConnectionP.Closed -= Conn_Closed;
                hubConnectionP.Closed += Conn_Closed;

            }catch(Exception ex)
            {
            }

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
            hubConnectionP.StopAsync();
            hubConnectionP = null;
            return GetHubConnection();
        }
    }
}
