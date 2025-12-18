using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Context.Template;

namespace Template.Infrastructure.Persistence.Repository
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContextSqlServerTemplate _context;

        public OrderRepository(ApplicationDbContextSqlServerTemplate context) => _context = context;

        public async Task<List<Domain.Orders.Order>> GetAllAsync()
        {
            return await _context.Orders
                .Select(x => Domain.Orders.Order.Load(
                    x.Id,
                    x.Name))
                .ToListAsync();
        }

        public async Task<Domain.Orders.Order?> GetByIdAsync(int id)
        {
            var entity = await _context.Orders.FindAsync(id);

            if (entity is null)
                return null;

            return Domain.Orders.Order.Load(
                entity.Id,
                entity.Name);
        }

        public async Task AddAsync(Domain.Orders.Order order)
        {
            var entity = new Persistence.Models.Entities.Template.Order
            {
                Name = order.CustomerName
            };

            _context.Orders.Add(entity);
        }

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}
