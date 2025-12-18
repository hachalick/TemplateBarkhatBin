using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;
using Template.Domain.Orders.Events;

namespace Template.Domain.Orders
{
    public class Order: AggregateRoot
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; }

        private Order() { }

        private Order(int id, string customerName)
        {
            Id = id;
            CustomerName = customerName;
        }

        public static Order Load(
            int id,
            string customerName)
            => new Order(id, customerName);

        public static Order Create(
            string customerName)
        {
            var order = new Order
            {
                CustomerName = customerName
            };

            order.AddDomainEvent(
                new OrderCreatedDomainEvent(order.Id, customerName));

            return order;
        }
    }
}
