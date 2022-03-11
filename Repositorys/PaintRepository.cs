using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class PaintRepository<T> : IPaintRepository<Paint> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public PaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Paint Get(int id)
        {
            return _context.Paint.Single(b => b.Id == id);
        }
    }
}
