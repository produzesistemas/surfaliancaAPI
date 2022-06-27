using Models;
using System;
using System.Linq;
using UnitOfWork;


namespace Repositorys
{
    public class ProductStatusRepository : IProductStatusRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public ProductStatusRepository(ApplicationDbContext context)
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

        public IQueryable<ProductStatus> GetAll()
        {
            return _context.ProductStatus.AsQueryable();
        }

    }
}
