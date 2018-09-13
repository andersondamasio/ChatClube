using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace com.chatclube.Services
{
    class SalaService<T> where T : class
    {

        HttpClient client;

        public SalaService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://chatclube.azurewebsites.net");
        }

        public async Task<bool> AddAsync(T item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/sala", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<T> GetSalasAsync(string IDSala = "")
        {
            var response = await client.GetStringAsync($"api/sala/{IDSala}");

            var listSalas = JsonConvert.DeserializeObject<T>(response);

            return listSalas;
        }
    }
}