using System;
using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public StoreRepository(ApplicationDbContext context)
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

        public Store Get()
        {
            return _context.Store.FirstOrDefault();
        }

        public void Update(Store entity)
        {

            var entityBase = _context.Store.Single(x => x.Id == entity.Id);

            entityBase.Name = entity.Name;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
