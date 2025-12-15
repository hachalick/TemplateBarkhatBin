using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities;

namespace Template.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderAsync(int id);
        Task AddOrderAsync(Order order);
    }
}
