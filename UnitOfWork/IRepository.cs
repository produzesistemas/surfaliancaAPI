using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T entity);
        IQueryable<T> GetAll();
        T Get(int id);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Where(Func<T, bool> expression);

    }
}
