using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class CupomRepository<T> : ICupomRepository<Coupon> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public CupomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Coupon Get(int id)
        {
            return _context.Cupom.Single(x => x.Id == id);
        }

        public IQueryable<Coupon> Where(Expression<Func<Coupon, bool>> expression)
        {
            return _context.Cupom.Where(expression).AsQueryable();
        }

        public IQueryable<Coupon> GetAll()
        {
            return _context.Cupom.AsQueryable();
        }
    }
}
