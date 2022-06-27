using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IBoardModelRepository : IDisposable
    {
        IQueryable<BoardModel> GetAll();
        BoardModel Get(int id);
        IQueryable<BoardModel> Where(Expression<Func<BoardModel, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(BoardModel entity);
        void Insert(BoardModel entity);
    }
}
