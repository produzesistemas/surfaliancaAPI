using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class StoreRepository<T> : IStoreRepository<Store> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Store> entities;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Store>();
        }

        public Store Get()
        {
            using (_context)
            {
                return entities.FirstOrDefault();
            }
        }
    }
}
