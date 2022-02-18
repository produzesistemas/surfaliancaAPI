using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class BoardModelRepository<T> : IBoardModelRepository<BoardModel> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public BoardModel Get(int id)
        {
                var b = _context.BoardModel.Single(b => b.Id == id);
                _context.Entry(b).Collection(b => b.BoardModelDimensions).Query().Load();
                return b;
        }

        public void Update(BoardModel entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
