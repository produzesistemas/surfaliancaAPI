using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ITailReinforcementRepository : IDisposable
    { 
        IQueryable<TailReinforcement> GetAll();
        TailReinforcement Get(int id);
        IQueryable<TailReinforcement> Where(Expression<Func<TailReinforcement, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(TailReinforcement entity);
        void Insert(TailReinforcement entity);
    }
}
