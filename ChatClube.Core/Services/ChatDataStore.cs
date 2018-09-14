using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using chatclube.com.Models;
using System.Net.Http.Headers;
using com.chatclube.Utils;
using com.chatclube.SalaX;
using System.Linq;

namespace chatclube.com.Services
{
    public class ChatDataStore<T>
       where T : class
    {
        private HttpClient client;
        private static ChatDataStore<T> instance = null;
        private string nomeClass = GetElementType(typeof(T)).ToLower();

        public static ChatDataStore<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChatDataStore<T>();
                }
                return instance;
            }
        }

        public ChatDataStore()
        {
            client = new HttpClient();
            if (Geral.IsDebug())
                client.BaseAddress = new Uri("http://192.168.1.6:51042");
            else
                client.BaseAddress = new Uri("https://chatclube.azurewebsites.net");
        }

        public async Task<bool> AddAsync(T item)
        {
            if (item == null)
                return false;
            var serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/{nomeClass}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<T> GetAsync(string ID = "")
        {

            var response = await client.GetStringAsync($"api/{nomeClass}/{ID}");
            var listSalas = JsonConvert.DeserializeObject<T>(response);

            return listSalas;
        }
        internal static string GetElementType(Type type)
        {
            //use type.GenericTypeArguments if exist 
            if (type.GenericTypeArguments.Any())
                return type.GenericTypeArguments.First().Name;

            return type.Name;
        }

    }
}