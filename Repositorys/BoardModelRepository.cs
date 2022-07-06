using Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Repositorys
{
    public class BoardModelRepository : IBoardModelRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<BoardModel> GetAll()
        {
            return _context.BoardModel.AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.BoardModel.Single(x => x.Id == id);
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
        }

        public void Delete(int id)
        {
            var dimensions = _context.BoardModelDimensions.Where(c => c.BoardModelId == id);
            var constructions = _context.BoardModelConstruction.Where(c => c.BoardModelId == id);
            var bottons = _context.BoardModelBottom.Where(c => c.BoardModelId == id);
            var fins = _context.BoardModelFinSystem.Where(c => c.BoardModelId == id);
            var laminations = _context.BoardModelLamination.Where(c => c.BoardModelId == id);
            var stringers = _context.BoardModelStringer.Where(c => c.BoardModelId == id);
            var tails = _context.BoardModelTail.Where(c => c.BoardModelId == id);
            var tailsR = _context.BoardModelTailReinforcement.Where(c => c.BoardModelId == id);

            _context.RemoveRange(dimensions);
            _context.RemoveRange(constructions);
            _context.RemoveRange(bottons);
            _context.RemoveRange(fins);
            _context.RemoveRange(laminations);
            _context.RemoveRange(stringers);
            _context.RemoveRange(tails);
            _context.RemoveRange(tailsR);

            var entity = _context.BoardModel.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public BoardModel Get(int id)
        {
               return _context.BoardModel
                .Include(c => c.BoardModelDimensions)
                .Include(c => c.BoardModelBottoms)
                .Include(c => c.BoardModelConstructions)
                .Include(c => c.BoardModelFinSystems)
                .Include(c => c.BoardModelLaminations)
                .Include(c => c.BoardModelStringers)
                .Include(c => c.BoardModelTailReinforcements)
                .Include(c => c.BoardModelTails)
                .Single(b => b.Id == id);
        }

        public void Update(BoardModel entity)
        {
            var entityBase = Get(entity.Id);
            entityBase.Description = entity.Description;
            entityBase.Name = entity.Name;
            entityBase.Value = entity.Value;
            entityBase.DaysProduction = entity.DaysProduction;
            entityBase.UrlMovie = entity.UrlMovie;
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;
            entityBase.UpdateApplicationUserId = entity.UpdateApplicationUserId;

            var toDeleteBottom = entityBase.BoardModelBottoms.Except(entity.BoardModelBottoms, new EqualityComparerBoardModelBottom()).ToList();
            var toInsertBottom = entity.BoardModelBottoms.Except(entityBase.BoardModelBottoms, new EqualityComparerBoardModelBottom()).ToList();
            toDeleteBottom.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertBottom.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelBottom.Add(x);
            });

            var toDeleteConstruction = entityBase.BoardModelConstructions.Except(entity.BoardModelConstructions, new EqualityComparerBoardModelConstruction()).ToList();
            var toInsertConstruction = entity.BoardModelConstructions.Except(entityBase.BoardModelConstructions, new EqualityComparerBoardModelConstruction()).ToList();
            toDeleteConstruction.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertConstruction.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelConstruction.Add(x);
            });

            var toDeleteDimension = entityBase.BoardModelDimensions.Except(entity.BoardModelDimensions, new EqualityComparerBoardModelDimension()).ToList();
            var toInsertDimension = entity.BoardModelDimensions.Except(entityBase.BoardModelDimensions, new EqualityComparerBoardModelDimension()).ToList();
            toDeleteDimension.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertDimension.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelDimensions.Add(x);
            });

            var toDeleteFinSystem = entityBase.BoardModelFinSystems.Except(entity.BoardModelFinSystems, new EqualityComparerBoardModelFinSystem()).ToList();
            var toInsertFinSystem = entity.BoardModelFinSystems.Except(entityBase.BoardModelFinSystems, new EqualityComparerBoardModelFinSystem()).ToList();
            toDeleteFinSystem.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertFinSystem.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelFinSystem.Add(x);
            });

            var toDeleteLamination = entityBase.BoardModelLaminations.Except(entity.BoardModelLaminations, new EqualityComparerBoardModelLamination()).ToList();
            var toInsertLamination = entity.BoardModelLaminations.Except(entityBase.BoardModelLaminations, new EqualityComparerBoardModelLamination()).ToList();
            toDeleteLamination.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertLamination.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelLamination.Add(x);
            });


            var toDeleteStringer = entityBase.BoardModelStringers.Except(entity.BoardModelStringers, new EqualityComparerBoardModelStringer()).ToList();
            var toInsertStringer = entity.BoardModelStringers.Except(entityBase.BoardModelStringers, new EqualityComparerBoardModelStringer()).ToList();
            toDeleteStringer.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertStringer.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelStringer.Add(x);
            });

            var toDeleteTailReinforcement = entityBase.BoardModelTailReinforcements.Except(entity.BoardModelTailReinforcements, new EqualityComparerBoardModelTailReinforcement()).ToList();
            var toInsertTailReinforcement = entity.BoardModelTailReinforcements.Except(entityBase.BoardModelTailReinforcements, new EqualityComparerBoardModelTailReinforcement()).ToList();
            toDeleteTailReinforcement.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertTailReinforcement.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelTailReinforcement.Add(x);
            });

            var toDeleteTail = entityBase.BoardModelTails.Except(entity.BoardModelTails, new EqualityComparerBoardModelTail()).ToList();
            var toInsertTail = entity.BoardModelTails.Except(entityBase.BoardModelTails, new EqualityComparerBoardModelTail()).ToList();
            toDeleteTail.ForEach(x =>
            {
                _context.Remove(x);
            });
            toInsertTail.ForEach(x =>
            {
                x.BoardModelId = entityBase.Id;
                x.Id = 0;
                _context.BoardModelTail.Add(x);
            });

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<BoardModel> Where(Expression<Func<BoardModel, bool>> expression)
        {
            return _context.BoardModel.Where(expression).AsQueryable();
        }

        public void Insert(BoardModel entity)
        {
            _context.BoardModel.Add(entity);
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        class EqualityComparerBoardModelBottom : IEqualityComparer<BoardModelBottom>
        {
            public bool Equals(BoardModelBottom x, BoardModelBottom y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.BottomId == y.BottomId;
            }

            public int GetHashCode(BoardModelBottom obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelConstruction : IEqualityComparer<BoardModelConstruction>
        {
            public bool Equals(BoardModelConstruction x, BoardModelConstruction y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.ConstructionId == y.ConstructionId;
            }

            public int GetHashCode(BoardModelConstruction obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelDimension : IEqualityComparer<BoardModelDimensions>
        {
            public bool Equals(BoardModelDimensions x, BoardModelDimensions y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.Description == y.Description;
            }

            public int GetHashCode(BoardModelDimensions obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelFinSystem : IEqualityComparer<BoardModelFinSystem>
        {
            public bool Equals(BoardModelFinSystem x, BoardModelFinSystem y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.FinSystemId == y.FinSystemId;
            }

            public int GetHashCode(BoardModelFinSystem obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelLamination : IEqualityComparer<BoardModelLamination>
        {
            public bool Equals(BoardModelLamination x, BoardModelLamination y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.LaminationId == y.LaminationId;
            }

            public int GetHashCode(BoardModelLamination obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelStringer : IEqualityComparer<BoardModelStringer>
        {
            public bool Equals(BoardModelStringer x, BoardModelStringer y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.StringerId == y.StringerId;
            }

            public int GetHashCode(BoardModelStringer obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelTailReinforcement : IEqualityComparer<BoardModelTailReinforcement>
        {
            public bool Equals(BoardModelTailReinforcement x, BoardModelTailReinforcement y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.TailReinforcementId == y.TailReinforcementId;
            }

            public int GetHashCode(BoardModelTailReinforcement obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelTail : IEqualityComparer<BoardModelTail>
        {
            public bool Equals(BoardModelTail x, BoardModelTail y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId && x.TailId == y.TailId;
            }

            public int GetHashCode(BoardModelTail obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
