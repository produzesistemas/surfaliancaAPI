using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;
using System;
using System.Linq.Expressions;

namespace Repositorys
{
    public class PaintRepository : IPaintRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public PaintRepository(ApplicationDbContext context)
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

        public IQueryable<Paint> GetAll()
        {
            return _context.Paint.AsQueryable();
        }

        public Paint Get(int id)
        {
            return _context.Paint.Single(b => b.Id == id);
        }

        public IQueryable<Paint> Where(Expression<Func<Paint, bool>> expression)
        {
            return _context.Paint.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Paint.Single(x => x.Id == id);
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
            if (_context.OrderProductOrdered.Any(c => c.PaintId == id))
            {
                throw new Exception("A FinSystem não pode ser excluída.Está relacionado com um pedido.Considere desativar!");
            };

            var entity = _context.Paint.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Paint entity)
        {
            var entityBase = _context.Paint.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            if (entity.Value.HasValue) { entityBase.Value = entity.Value.Value; } else { entityBase.Value = null; }
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Paint entity)
        {
            _context.Paint.Add(entity);
            _context.SaveChanges();
        }
    }
}
