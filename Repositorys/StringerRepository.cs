using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UnitOfWork;
using System;
using System.Linq.Expressions;

namespace Repositorys
{
    public class StringerRepository : IStringerRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public StringerRepository(ApplicationDbContext context)
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

        public IQueryable<Stringer> GetAll()
        {
            return _context.Stringer.AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Stringer.Single(x => x.Id == id);
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
            if (_context.OrderProductOrdered.Any(c => c.StringerId == id))
            {
                throw new Exception("A longarina não pode ser excluída.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            };
            var entity = _context.Stringer.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public Stringer Get(int id)
        {
            return _context.Stringer.Single(b => b.Id == id);
        }

        public void Update(Stringer entity)
        {

            var entityBase = _context.Stringer.Single(x => x.Id == entity.Id);

            entityBase.Name = entity.Name;
            entityBase.Details = entity.Details;
            if (entity.Value.HasValue) { entityBase.Value = entity.Value.Value; } else { entityBase.Value = null; }
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<Stringer> Where(Expression<Func<Stringer, bool>> expression)
        {
            return _context.Stringer.Where(expression).AsQueryable();
        }

        public void Insert(Stringer entity)
        {
            _context.Stringer.Add(entity);
            _context.SaveChanges();
        }
    }
}
