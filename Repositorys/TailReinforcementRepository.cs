using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;


namespace Repositorys
{
    public class TailReinforcementRepository : ITailReinforcementRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public TailReinforcementRepository(ApplicationDbContext context)
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

        public IQueryable<TailReinforcement> GetAll()
        {
            var lst = new List<TailReinforcement>();

            using (var db = _context)
            {
                lst = db.TailReinforcement.ToList();
            }

            return lst.AsQueryable();
        }

        public TailReinforcement Get(int id)
        {
            return _context.TailReinforcement
    .Single(x => x.Id == id);
        }

        public IQueryable<TailReinforcement> Where(Expression<Func<TailReinforcement, bool>> expression)
        {
            return _context.TailReinforcement.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.TailReinforcement.Single(x => x.Id == id);
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
            //if (_context.OrderProductOrdered.Any(c => c.ConstructionId == id))
            //{
            //    throw new Exception("A tecnologia / construção não pode ser excluída.Está relacionado com um pedido.Considere desativar!");
            //};

            var entity = _context.TailReinforcement.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public void Update(TailReinforcement entity)
        {
            var entityBase = _context.TailReinforcement.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            entityBase.Details = entity.Details;
            if (entity.Value.HasValue) { entityBase.Value = entity.Value.Value; } else { entityBase.Value = null; }
            entityBase.UpdateDate = DateTime.Now;
            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(TailReinforcement entity)
        {
            _context.TailReinforcement.Add(entity);
            _context.SaveChanges();
        }
    }
}
