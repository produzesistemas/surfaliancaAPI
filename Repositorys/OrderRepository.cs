using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Repositorys
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Order Get(int id)
        {
            var b = _context.Order.Single(b => b.Id == id);
            return b;
        }

        public IQueryable<Order> Where(Expression<Func<Order, bool>> expression)
        {
            return _context.Order.Include(x => x.PaymentCondition).Where(expression).AsQueryable();
        }

        public void Insert(Order entity)
        {
            _context.Order.Add(entity);
            _context.SaveChanges();
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Order.AsQueryable();
        }

        public void Update(Order entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
