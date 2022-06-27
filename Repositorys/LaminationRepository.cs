using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;
using System;
using System.Linq.Expressions;

namespace Repositorys
{
    public class LaminationRepository : ILaminationRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public LaminationRepository(ApplicationDbContext context)
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

        public IQueryable<Lamination> GetAll()
        {
            return _context.Lamination.AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Lamination.Single(x => x.Id == id);
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
            if (_context.OrderProductOrdered.Any(c => c.LaminationId == id))
            {
                throw new Exception("O modelo não pode ser excluído.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            };

            var entity = _context.Lamination.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public Lamination Get(int id)
        {
            return _context.Lamination.Single(b => b.Id == id);
        }

        public void Update(Lamination entity)
        {
            entity.Name = entity.Name;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<Lamination> Where(Expression<Func<Lamination, bool>> expression)
        {
            return _context.Lamination.Where(expression).AsQueryable();
        }

        public void Insert(Lamination entity)
        {
            _context.Lamination.Add(entity);
            _context.SaveChanges();
        }
    }
}
