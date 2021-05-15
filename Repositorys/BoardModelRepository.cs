using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class BoardModelRepository<T> : IBoardModelRepository<BoardModel> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<BoardModel> entities;

        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<BoardModel>();
        }

        public BoardModel Get(int id)
        {
            using (_context)
            {
                var c = entities.Include(x => x.BoardModelBoardTypes).ThenInclude(x => x.BoardType)
                    .Include(x => x.BoardModelBottoms).ThenInclude(x => x.Bottom)
                    .Include(x => x.BoardModelConstructions).ThenInclude(x => x.Construction)
                    .Include(x => x.BoardModelFinSystems).ThenInclude(x => x.FinSystem)
                    .Include(x => x.BoardModelLaminations).ThenInclude(x => x.Lamination)
                    .Include(x => x.BoardModelLitigations).ThenInclude(x => x.Litigation)
                    .Include(x => x.BoardModelShapers).ThenInclude(x => x.Shaper)
                    .Include(x => x.BoardModelSizes).ThenInclude(x => x.Size)
                    .Include(x => x.BoardModelTails).ThenInclude(x => x.Tail)
                    .Include(x => x.BoardModelWidths).ThenInclude(x => x.Width)
                    .FirstOrDefault(x => x.Id == id);

                return c;
            }
        }
    }
}
