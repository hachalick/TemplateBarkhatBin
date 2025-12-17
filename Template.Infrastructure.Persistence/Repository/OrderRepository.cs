using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities;

namespace Template.Infrastructure.Persistence.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly TemplateBarkhatBinContext _context;
        public OrderRepository(TemplateBarkhatBinContext context) => _context = context;

        public async Task<List<Order>> GetAllAsync() => await _context.Orders.ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) => await _context.Orders.FindAsync(id);

        public async Task AddAsync(Order order) { _context.Orders.Add(order); await _context.SaveChangesAsync(); }
    }
}
