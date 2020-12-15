using System;
using System.Collections.Generic;

namespace ToDoList.DAL.Interfaces
{
    public interface IRepository<T,TU> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(TU param);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
