using Models;
using UnitOfWork;
using System;

namespace Repositorys
{
    public class OrderTrackingRepository : IOrderTrackingRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public OrderTrackingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Insert(OrderTracking entity)
        {
            _context.OrderTracking.Add(entity);
            _context.SaveChanges();
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
    }
}
