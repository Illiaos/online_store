using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Memory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = new List<Order>();

        public Order Create()
        {
            int nextId = orders.Count + 1;
            Order order = new Order(nextId, new List<OrderItem>());
            orders.Add(order);
            return order;
        }

        public Order GetById(int id)
        {
            return orders.Single(item => item.Id == id);
        }

        public bool Update(Order order)
        {
            return true;
        }
    }
}
