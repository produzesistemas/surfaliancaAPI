using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class ShippingCompanyRepository : IShippingCompanyRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public ShippingCompanyRepository(ApplicationDbContext context)
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

        public IQueryable<ShippingCompany> GetAll()
        {
            return _context.ShippingCompany.AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.ShippingCompany.Single(x => x.Id == id);
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
            //if (_context.OrderProduct.Any(c => c.ProductId == id))
            //{
            //    throw new Exception("O modelo não pode ser excluído.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            //};
            var entity = _context.ShippingCompany.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public ShippingCompany Get(int id)
        {
            return _context.ShippingCompany.Single(b => b.Id == id);
        }

        public void Update(ShippingCompany entity)
        {

            var entityBase = _context.ShippingCompany.Single(x => x.Id == entity.Id);

            entityBase.Name = entity.Name;
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<ShippingCompany> Where(Expression<Func<ShippingCompany, bool>> expression)
        {
            return _context.ShippingCompany.Where(expression).AsQueryable();
        }

        public void Insert(ShippingCompany entity)
        {
            _context.ShippingCompany.Add(entity);
            _context.SaveChanges();
        }
    }
}
