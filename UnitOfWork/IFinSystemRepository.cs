using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IFinSystemRepository : IDisposable
    {
        IQueryable<FinSystem> GetAll();
        FinSystem Get(int id);
        IQueryable<FinSystem> Where(Expression<Func<FinSystem, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(FinSystem entity);
        void Insert(FinSystem entity);
    }
}
