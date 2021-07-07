using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class OrderRepository<T> : IOrderRepository<Order> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Order>();
        }

        public Order Get(int id)
        {
            var b = _context.Order.Single(b => b.Id == id);
            return b;
        }
    }
}
