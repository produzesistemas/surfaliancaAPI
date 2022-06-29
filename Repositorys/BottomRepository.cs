using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class BottomRepository : IBottomRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public BottomRepository(ApplicationDbContext context)
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

        public IQueryable<Bottom> GetAll()
        {
            return _context.Bottom.AsQueryable();
        }

        public Bottom Get(int id)
        {
            return _context.Bottom.Single(x => x.Id == id);
        }

        public IQueryable<Bottom> Where(Expression<Func<Bottom, bool>> expression)
        {
            return _context.Bottom.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Bottom.Single(x => x.Id == id);
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
            if (_context.OrderProductOrdered.Any(c => c.BottomId == id))
            {
                throw new Exception("O fundo não pode ser excluído.Está relacionado com um pedido.Considere desativar!");
            };

            var entity = _context.Bottom.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public void Update(Bottom entity)
        {
            var entityBase = _context.Bottom.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;
            entityBase.UpdateApplicationUserId = entity.UpdateApplicationUserId;
            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Bottom entity)
        {
            _context.Bottom.Add(entity);
            _context.SaveChanges();
        }
    }
}
