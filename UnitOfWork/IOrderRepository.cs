﻿

using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IOrderRepository<T> where T : BaseEntity
    {
        T Get(int id);
        IQueryable<T> GetByUser(string id);
    }
}
