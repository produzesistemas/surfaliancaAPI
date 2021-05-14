using Models;
using System.Linq;
using UnitOfWork;

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
            using (_context)
            {
                var b = _context.BoardModel.Single(b => b.Id == id);
                _context.Entry(b).Collection(b => b.BoardModelBoardTypes).Load();
                _context.Entry(b).Collection(b => b.BoardModelBottoms).Load();
                _context.Entry(b).Collection(b => b.BoardModelConstructions).Load();
                _context.Entry(b).Collection(b => b.BoardModelFinSystems).Load();
                _context.Entry(b).Collection(b => b.BoardModelLaminations).Load();
                _context.Entry(b).Collection(b => b.BoardModelLitigations).Load();
                _context.Entry(b).Collection(b => b.BoardModelShapers).Load();
                _context.Entry(b).Collection(b => b.BoardModelSizes).Load();
                _context.Entry(b).Collection(b => b.BoardModelTails).Load();
                _context.Entry(b).Collection(b => b.BoardModelWidths).Load();
                return b;
            }
        }
    }
}
