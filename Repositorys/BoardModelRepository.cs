
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class BoardModelRepository<T> : IBoardModelRepository<BoardModel> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        //private DbSet<BoardModel> entities;
        //private DbSet<Shaper> shapers;
        //private DbSet<Size> sizes;
        //private DbSet<BoardType> boardTypes;
        //private DbSet<Bottom> bottoms;
        //private DbSet<Litigation> litigations;
        //private DbSet<Lamination> laminations;
        //private DbSet<Width> widths;
        //private DbSet<Tail> tails;
        //private DbSet<Construction> constructions;
        //private DbSet<FinSystem> finSystems;
        //private DbSet<IdentityUser> users;
        //private DbSet<BoardModelShaper> boardModelShapers;
        //private DbSet<BoardModelTail> boardModelTails;
        //private DbSet<BoardModelConstruction> boardModelConstructions;
        //private DbSet<BoardModelLamination> boardModelLaminations;
        //private DbSet<BoardModelLitigation> boardModelLitigations;
        //private DbSet<BoardModelSize> boardModelSizes;
        //private DbSet<BoardModelWidth> boardModelWidths;
        //private DbSet<BoardModelBoardType> boardModelBoardTypes;
        //private DbSet<BoardModelBottom> boardModelBottoms;
        //private DbSet<BoardModelFinSystem> boardModelFinSystems;
        //private DbSet<BoardModelImage> boardModelImages;

        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
            //entities = context.Set<BoardModel>();
            //shapers = context.Set<Shaper>();
            //sizes = context.Set<Size>();
            //boardTypes = context.Set<BoardType>();
            //bottoms = context.Set<Bottom>();
            //litigations = context.Set<Litigation>();
            //constructions = context.Set<Construction>();
            //widths = context.Set<Width>();
            //tails = context.Set<Tail>();
            //laminations = context.Set<Lamination>();
            //finSystems = context.Set<FinSystem>();
            //users = context.Set<IdentityUser>();
            //boardModelShapers = context.Set<BoardModelShaper>();
            //boardModelTails = context.Set<BoardModelTail>();
            //boardModelConstructions = context.Set<BoardModelConstruction>();
            //boardModelLaminations = context.Set<BoardModelLamination>();
            //boardModelLitigations = context.Set<BoardModelLitigation>();
            //boardModelSizes = context.Set<BoardModelSize>();
            //boardModelWidths = context.Set<BoardModelWidth>();
            //boardModelBoardTypes = context.Set<BoardModelBoardType>();
            //boardModelBottoms = context.Set<BoardModelBottom>();
            //boardModelFinSystems = context.Set<BoardModelFinSystem>();
            //boardModelImages = context.Set<BoardModelImage>();
        }

        public BoardModel Get(int id)
        {
            using (_context)
            {
                var b = _context.BoardModel
                    .Single(b => b.Id == id);

                _context.Entry(b)
                    .Collection(b => b.BoardModelBoardTypes)
                    .Load();

                _context.Entry(b)
                    .Collection(b => b.BoardModelBottoms)
                    .Load();

                _context.Entry(b)
    .Collection(b => b.BoardModelConstructions)
    .Load();
                _context.Entry(b)
    .Collection(b => b.BoardModelFinSystems)
    .Load();
                _context.Entry(b)
    .Collection(b => b.BoardModelLaminations)
    .Load();
                _context.Entry(b)
    .Collection(b => b.BoardModelLitigations)
    .Load();
                _context.Entry(b)
    .Collection(b => b.BoardModelShapers)
    .Load();
                _context.Entry(b)
    .Collection(b => b.BoardModelSizes)
    .Load();

                _context.Entry(b)
.Collection(b => b.BoardModelTails)
.Load();

                _context.Entry(b).Collection(b => b.BoardModelWidths).Load();

                return b;
            }

            //return entities.Select(x => new BoardModel
            //{
            //    Id = x.Id,
            //    Description = x.Description,
            //    Value = x.Value,
            //    CreateDate = x.CreateDate,
            //    UpdateDate = x.UpdateDate,
            //    ApplicationUserId = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).Id,
            //    UpdateApplicationUserId = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).Id,
            //    CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
            //    AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,

            //    BoardModelBoardTypes = boardModelBoardTypes.Where(b => b.BoardModelId == x.Id),
            //    BoardModelBottoms = boardModelBottoms.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelConstructions = boardModelConstructions.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelFinSystems = boardModelFinSystems.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelLaminations = boardModelLaminations.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelLitigations = boardModelLitigations.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelShapers = boardModelShapers.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelSizes = boardModelSizes.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelTails = boardModelTails.Where(b => b.BoardModelId == x.Id).ToList(),
            //    BoardModelWidths = boardModelWidths.Where(b => b.BoardModelId == x.Id).ToList()


            //    //BoardModelBoardTypes = boardModelBoardTypes.Select(c => new BoardModelBoardType
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    BoardTypeId = c.BoardTypeId,
            //    //    BoardType = boardTypes.FirstOrDefault(s => s.Id == c.BoardTypeId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelBottoms = boardModelBottoms.Select(c => new BoardModelBottom
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    BottomId = c.BottomId,
            //    //    Bottom = bottoms.FirstOrDefault(s => s.Id == c.BottomId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelConstructions = boardModelConstructions.Select(c => new BoardModelConstruction
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    ConstructionId = c.ConstructionId,
            //    //    Construction = constructions.FirstOrDefault(s => s.Id == c.ConstructionId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelFinSystems = boardModelFinSystems.Select(c => new BoardModelFinSystem
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    FinSystemId = c.FinSystemId,
            //    //    FinSystem = finSystems.FirstOrDefault(s => s.Id == c.FinSystemId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelLaminations = boardModelLaminations.Select(c => new BoardModelLamination
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    LaminationId = c.LaminationId,
            //    //    Lamination = laminations.FirstOrDefault(s => s.Id == c.LaminationId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelLitigations = boardModelLitigations.Select(c => new BoardModelLitigation
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    LitigationId = c.LitigationId,
            //    //    Litigation = litigations.FirstOrDefault(s => s.Id == c.LitigationId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelShapers = boardModelShapers.Select(c => new BoardModelShaper
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    ShaperId = c.ShaperId,
            //    //    Shaper = shapers.FirstOrDefault(s => s.Id == c.ShaperId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelSizes = boardModelSizes.Select(c => new BoardModelSize
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    SizeId = c.SizeId,
            //    //    Size = sizes.FirstOrDefault(s => s.Id == c.SizeId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelTails = boardModelTails.Select(c => new BoardModelTail
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    TailId = c.TailId,
            //    //    Tail = tails.FirstOrDefault(s => s.Id == c.TailId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList(),
            //    //BoardModelWidths = boardModelWidths.Select(c => new BoardModelWidth
            //    //{
            //    //    Id = c.Id,
            //    //    BoardModelId = c.BoardModelId,
            //    //    WidthId = c.WidthId,
            //    //    Width = widths.FirstOrDefault(s => s.Id == c.WidthId)
            //    //}).Where(b => b.BoardModelId == x.Id).ToList()
            //}).FirstOrDefault(x => x.Id == id);
        }
    }
}
