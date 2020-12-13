using System;
using System.Collections.Generic;

namespace ToDoList.DAL.Interfaces
{
    public interface IRepository<T,U> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(U param);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
