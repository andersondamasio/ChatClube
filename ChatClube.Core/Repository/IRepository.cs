using System;
using System.Collections.Generic;
using System.Text;

namespace com.chatclube.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        T Remove(string key);
        T Get(string id);
        IEnumerable<T> GetAll();
    }
}
