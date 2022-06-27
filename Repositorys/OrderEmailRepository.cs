using Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorys
{
    public class OrderEmailRepository : IOrderEmailRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public OrderEmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Insert(OrderEmail entity)
        {
            _context.OrderEmail.Add(entity);
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
