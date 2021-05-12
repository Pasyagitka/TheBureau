using System.Collections.Generic;
using System.Data.Entity;

namespace TheBureau.Repositories
{
    interface IRepository<T> where T : class
    { 
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T item);
        //void Update(int id, T item);
        void Delete(int id);
    }
}