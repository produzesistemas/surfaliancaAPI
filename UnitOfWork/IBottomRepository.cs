
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IBottomRepository : IDisposable
    {
        IQueryable<Bottom> GetAll();
        Bottom Get(int id);
        IQueryable<Bottom> Where(Expression<Func<Bottom, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Bottom entity);
        void Insert(Bottom entity);
    }
}
