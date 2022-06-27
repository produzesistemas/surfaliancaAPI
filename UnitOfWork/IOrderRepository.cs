using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IOrderRepository : IDisposable
    {
        IQueryable<Order> GetAll();
        Order Get(int id);
        IQueryable<Order> Where(Expression<Func<Order, bool>> expression);
        void Insert(Order entity);
        void Update(Order entity);
    }
}
