using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IStoreRepository : IDisposable
    {
        IQueryable<Store> GetAll();
        Store Get(int id);
        IQueryable<Store> Where(Expression<Func<Store, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Store entity);
        void Insert(Store entity);
    }
}
