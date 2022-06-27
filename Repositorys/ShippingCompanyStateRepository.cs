using Models;
using System;
using System.Linq;
using UnitOfWork;
namespace Repositorys
{
    public class ShippingCompanyStateRepository : IShippingCompanyStateRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public ShippingCompanyStateRepository(ApplicationDbContext context)
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

        public void Delete(int id)
        {
            var entity = _context.ShippingCompanyState.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Insert(ShippingCompanyState entity)
        {
            _context.ShippingCompanyState.Add(entity);
            _context.SaveChanges();
        }

    }
}
