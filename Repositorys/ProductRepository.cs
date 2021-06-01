using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;


namespace Repositorys
{
    public class ProductRepository<T> : IProductRepository<Product> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Product> entities;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Product>();
        }

        public Product Get(int id)
        {
            using (_context)
            {
                var product = _context.Product.Single(b => b.Id == id);
                product.ProductStatus = _context.ProductStatus.Single(b => b.Id == product.ProductStatusId);
                product.ProductType = _context.ProductType.Single(b => b.Id == product.ProductTypeId);
                return product;
            }
        }
    }
}
