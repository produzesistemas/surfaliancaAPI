using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IBoardModelDimensionsRepository : IDisposable
    {
        IQueryable<BoardModelDimensions> Where(Expression<Func<BoardModelDimensions, bool>> expression);
        void Delete(int id);
        void Insert(BoardModelDimensions entity);
    }
}
