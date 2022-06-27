using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ITeamRepository : IDisposable
    {
        IQueryable<Team> GetAll();
        Team Get(int id);
        IQueryable<Team> Where(Expression<Func<Team, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Team entity);
        void Insert(Team entity);
    }
}
