using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.chatclube.Repository.Config
{
    public interface IRepository<T> where T : class, IEntity
    {
        int Add(T item);
        int Update(T item);
        int Remove(T key);
        T Get(string id);
        IQueryable<T> GetAll();
    }
}
