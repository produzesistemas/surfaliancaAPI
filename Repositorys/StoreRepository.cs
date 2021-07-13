using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class StoreRepository<T> : IStoreRepository<Store> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        //private DbSet<Store> entities;
        //private DbSet<Product> products;
        //private DbSet<BoardModel> boardModels;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
            //entities = context.Set<Store>();
            //products = context.Set<Product>();
            //boardModels = context.Set<BoardModel>();

        }

        public Store Get()
        {
                return _context.Store.FirstOrDefault();
        }
    }
}
