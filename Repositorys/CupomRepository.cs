using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class CupomRepository : ICupomRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public CupomRepository(ApplicationDbContext context)
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

        public IQueryable<Coupon> GetAll()
        {
            return _context.Coupon.AsQueryable();
        }

        public Coupon Get(int id)
        {
            return _context.Coupon
                .Single(x => x.Id == id);
        }

        public IQueryable<Coupon> Where(Expression<Func<Coupon, bool>> expression)
        {
            return _context.Coupon.Where(expression).AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Coupon.Single(x => x.Id == id);
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
            if (_context.Order.Any(c => c.CouponId == id))
            {
                throw new Exception("O cupom não pode ser excluído.Está relacionado com um pedido.Considere desativar!");
            };

            var entity = _context.Coupon.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public void Update(Coupon entity)
        {
            var entityBase = _context.Coupon.Single(x => x.Id == entity.Id);
            entityBase.Description = entity.Description;
            entityBase.ClientId = entity.ClientId;
            entityBase.Code = entity.Code;
            entityBase.FinalDate = entity.FinalDate;
            entityBase.InitialDate = entity.InitialDate;
            entityBase.Quantity = entity.Quantity;
            entityBase.General = entity.General;
            entityBase.Quantity = entity.Quantity;
            entityBase.Type =  entity.Type;
            entityBase.Value = entity.Value;
            entityBase.ValueMinimum = entity.ValueMinimum;
            entityBase.UpdateDate = DateTime.Now;
            entityBase.UpdateApplicationUserId = entity.UpdateApplicationUserId;
            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Coupon entity)
        {
            _context.Coupon.Add(entity);
            _context.SaveChanges();
        }
    }
}
