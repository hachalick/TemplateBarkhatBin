using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Orders;

namespace Template.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task AddAsync(Order order);

        Task SaveChangesAsync();
    }
}
