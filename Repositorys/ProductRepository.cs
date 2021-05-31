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
                return entities.Include(x => x.ProductStatus).Include(x => x.ProductType)
                    .FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
