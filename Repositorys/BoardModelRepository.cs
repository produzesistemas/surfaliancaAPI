using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class BoardModelRepository<T> : IBoardModelRepository<BoardModel> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        //private DbSet<BoardModel> entities;

        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
            //entities = context.Set<BoardModel>();
        }

        public BoardModel Get(int id)
        {

                var b = _context.BoardModel.Single(b => b.Id == id);
                _context.Entry(b).Collection(b => b.BoardModelBoardTypes).Query().Include(x => x.BoardType).Load();
                _context.Entry(b).Collection(b => b.BoardModelBottoms).Query().Include(x => x.Bottom).Load();
                _context.Entry(b).Collection(b => b.BoardModelConstructions).Query().Include(x => x.Construction).Load();
                _context.Entry(b).Collection(b => b.BoardModelLaminations).Query().Include(x => x.Lamination).Load();
                _context.Entry(b).Collection(b => b.BoardModelShapers).Query().Include(x => x.Shaper).Load();
                _context.Entry(b).Collection(b => b.BoardModelSizes).Query().Include(x => x.Size).Load();
                _context.Entry(b).Collection(b => b.BoardModelTails).Query().Include(x => x.Tail).Load();
                _context.Entry(b).Collection(b => b.BoardModelWidths).Query().Include(x => x.Width).Load();
                return b;
        }

        public void Update(BoardModel entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
