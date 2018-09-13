using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chatclube.com.Services
{
    public interface IChatDataStore<T>
    {
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<List<T>> GetSalasAsync(bool forceRefresh = false);
    }
}
