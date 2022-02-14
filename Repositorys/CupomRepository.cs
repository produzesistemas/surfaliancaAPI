using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class CupomRepository<T> : ICupomRepository<Cupom> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public CupomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cupom Get(int id)
        {
            return _context.Cupom.Single(x => x.Id == id);
        }

        public IQueryable<Cupom> Where(Expression<Func<Cupom, bool>> expression)
        {
            return _context.Cupom.Where(expression).AsQueryable();
        }

        public IQueryable<Cupom> GetAll()
        {
            return _context.Cupom.AsQueryable();
        }
    }
}
