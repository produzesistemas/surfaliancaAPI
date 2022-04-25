using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class ShippingCompanyRepository<T> : IShippingCompanyRepository<ShippingCompany> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public ShippingCompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ShippingCompany Get(int id)
        {
            var shipping =  _context.ShippingCompany.Include(x => x.ShippingCompanyStates).Single(x => x.Id == id);
            _context.Dispose();
            return shipping;
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
            _context.Dispose();
        }

        public void Delete(int id)
        {
            //if ((_context.ShippingCompanyState.Any(c => c.ShippingCompanyId == id)) ||
            //    (_context.Paint.Any(c => c.BoardModelId == id)))
            //{
            //    throw new Exception("A transportadora não pode ser excluído.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            //};

            var dimensions = _context.ShippingCompanyState.Where(c => c.ShippingCompanyId == id);
            _context.RemoveRange(dimensions);
            var entity = _context.ShippingCompany.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
