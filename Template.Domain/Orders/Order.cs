using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.Orders
{
    public class Order
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; }
        public decimal TotalPrice { get; private set; }

        private Order() { }

        public Order(string customerName, decimal totalPrice)
        {
            CustomerName = customerName;
            TotalPrice = totalPrice;
        }
    }
}
