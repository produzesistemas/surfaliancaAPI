using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ICupomRepository : IDisposable
    {
        IQueryable<Coupon> GetAll();
        Coupon Get(int id);
        IQueryable<Coupon> Where(Expression<Func<Coupon, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Coupon entity);
        void Insert(Coupon entity);
    }
}
