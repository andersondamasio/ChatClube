using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.chatclube.Repository.Config
{
    public interface IRepository<T> where T : class
    {
        System.Threading.Tasks.Task<int> AddAsync(T item);
        System.Threading.Tasks.Task<int> UpdateAsync(T item);
        System.Threading.Tasks.Task<int> RemoveAsync(T key);
        IQueryable<T> GetAll();
    }
}
