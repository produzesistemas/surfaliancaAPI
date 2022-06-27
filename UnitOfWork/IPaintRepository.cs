
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IPaintRepository : IDisposable
    {
        IQueryable<Paint> GetAll();
        Paint Get(int id);
        IQueryable<Paint> Where(Expression<Func<Paint, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Paint entity);
        void Insert(Paint entity);
    }
}
