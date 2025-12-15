using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.Repository;

namespace Template.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository) => _repository = repository;

        public async Task<List<Order>> GetOrdersAsync() => await _repository.GetAllAsync();
        public async Task<Order?> GetOrderAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddOrderAsync(Order order) => await _repository.AddAsync(order);
    }
}
