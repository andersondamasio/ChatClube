using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using chatclube.com.Models;

namespace chatclube.com.Services
{
  public class ChatDataStore<T> : IChatDataStore<T>
     where T : class
        {

            HttpClient client;
        IEnumerable<T> items;

        public ChatDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"http://192.168.1.6:5000");

            items = new List<T>();
        }

        public async Task<bool> AddAsync(T item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/sala", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}