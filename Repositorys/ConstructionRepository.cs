using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;


namespace Repositorys
{
    public class ConstructionRepository : IConstructionRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public ConstructionRepository(ApplicationDbContext context)
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

        public IQueryable<Construction> GetAll()
        {
            var lst = new List<Construction>();

            using (var db = _context)
            {
                lst = db.Construction.ToList();
            }

            return lst.AsQueryable();
        }

        public Construction Get(int id)
        {
            return _context.Construction
    .Single(x => x.Id == id);
        }

        public IQueryable<Construction> Where(Expression<Func<Construction, bool>> expression)
        {
            return _context.Construction.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Construction.Single(x => x.Id == id);
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
            if (_context.OrderProductOrdered.Any(c => c.ConstructionId == id))
            {
                throw new Exception("A tecnologia / construção não pode ser excluída.Está relacionado com um pedido.Considere desativar!");
            };

            var entity = _context.Construction.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public void Update(Construction entity)
        {
            var entityBase = _context.Construction.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            entityBase.UrlMovie = entity.UrlMovie;
            entityBase.Details = entity.Details;
            entityBase.Value = entity.Value;
            entityBase.UpdateDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Construction entity)
        {
            _context.Construction.Add(entity);
            _context.SaveChanges();
        }
    }
}
