using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.chatclube.Repository.Config
{
    public interface IRepository<T> where T : class
    {
        int Add(T item);
        int Update(T item);
        int Remove(T key);
        IQueryable<T> GetAll();
    }
}
