﻿using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class OrderRepository<T> : IOrderRepository<Order> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Order>();
        }

        public Order Get(int id)
        {
            var b = _context.Order.Single(b => b.Id == id);
            _context.Entry(b).Collection(b => b.OrderProduct).Query().Include(x => x.Product).Load();
            _context.Entry(b).Collection(b => b.OrderProductOrdered).Query()
                .Include(x => x.Width)
                .Include(x => x.BoardModel)
                .Include(x => x.Tail)
                .Include(x => x.Size)
                .Include(x => x.Shaper)
                .Include(x => x.Lamination)
                .Include(x => x.Construction)
                .Include(x => x.Bottom)
                .Include(x => x.BoardType)
                .Include(x => x.Finishing)
                .Include(x => x.Paint)
                .Load();
            _context.Entry(b).Collection(b => b.OrderTracking).Query()
                .Include(x => x.StatusOrder)
                .Include(x => x.StatusPaymentOrder)
                .Load();
            return b;
        }
    }
}
