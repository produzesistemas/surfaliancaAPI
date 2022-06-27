using Models;
using System;

namespace UnitOfWork
{
    public interface ITeamImageRepository : IDisposable
    {
        void Delete(int id);
        void Insert(TeamImage entity);
    }
}
