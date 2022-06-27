using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;
using System;
using System.Linq.Expressions;

namespace Repositorys
{
    public class TailRepository : ITailRepository, IDisposable
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;

        public TailRepository(ApplicationDbContext context)
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

        public IQueryable<Tail> GetAll()
        {
            return _context.Tail.AsQueryable();
        }

        public Tail Get(int id)
        {
            return _context.Tail.Single(b => b.Id == id);
        }

        public IQueryable<Tail> Where(Expression<Func<Tail, bool>> expression)
        {
            return _context.Tail.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Tail.Single(x => x.Id == id);
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
            if (_context.OrderProductOrdered.Any(c => c.TailId == id))
            {
                throw new Exception("A Rabeta não pode ser excluída.Está relacionado com um pedido.Considere desativar!");
            };

            var entity = _context.Tail.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Tail entity)
        {
            var entityBase = _context.Tail.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Tail entity)
        {
            _context.Tail.Add(entity);
            _context.SaveChanges();
        }
    }
}
