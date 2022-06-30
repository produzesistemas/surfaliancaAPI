using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class FinSystemRepository : IFinSystemRepository, IDisposable
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;

        public FinSystemRepository(ApplicationDbContext context)
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

        public IQueryable<FinSystem> GetAll()
        {
            return _context.FinSystem.AsQueryable();
        }

        public FinSystem Get(int id)
        {
            return _context.FinSystem.Single(b => b.Id == id);
        }

        public IQueryable<FinSystem> Where(Expression<Func<FinSystem, bool>> expression)
        {
            return _context.FinSystem.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.FinSystem.Single(x => x.Id == id);
            if (entity.Active)
            {
                entity.Active = false;
            }
            else
            {
                entity.Active = true;
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.FinSystem.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public void Update(FinSystem entity)
        {
            var entityBase = _context.FinSystem.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            if (entity.Value.HasValue) { entityBase.Value = entity.Value.Value; }
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(FinSystem entity)
        {
            _context.FinSystem.Add(entity);
            _context.SaveChanges();
        }
    }
}
