using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;


namespace Repositorys
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public ProductRepository(ApplicationDbContext context)
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

        public IQueryable<Product> GetAll()
        {
            return _context.Product.AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.Product.Single(x => x.Id == id);
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
            if (_context.OrderProduct.Any(c => c.ProductId == id))
            {
                throw new Exception("O modelo não pode ser excluído.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            };
            var entity = _context.Product.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public Product Get(int id)
        {
            return _context.Product.Single(b => b.Id == id);
        }

        public void Update(Product entity)
        {

            var entityBase = _context.Product.Single(x => x.Id == entity.Id);

            entityBase.Name = entity.Name;
            entityBase.Description = entity.Description;
            entityBase.ImageName1 = entity.ImageName1;
            entityBase.ImageName2 = entity.ImageName2;
            entityBase.IsPromotion = entity.IsPromotion;
            entityBase.IsSpotlight = entity.IsSpotlight;
            entityBase.ProductStatusId = entity.ProductStatusId;
            entityBase.ProductTypeId = entity.ProductTypeId;
            entityBase.Value = entity.Value;
            if (entity.ValuePromotion.HasValue) { entityBase.ValuePromotion = entity.ValuePromotion.Value; }
            entityBase.TypeSaleId = entity.TypeSaleId;
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _context.Product.Where(expression).AsQueryable();
        }

        public void Insert(Product entity)
        {
            _context.Product.Add(entity);
            _context.SaveChanges();
        }
    }
}
