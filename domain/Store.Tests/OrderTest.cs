using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable
namespace Store.Tests
{
    public class OrderTest
    {
        [Fact]
        public void Order_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(0, null));
        }

        [Fact]
        public void TotalCount_WithEmpyItems_ReturnZero()
        {
            Order order = new Order(1, new List<OrderItem>());
            Assert.Equal(0, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnZer()
        {
            Order order = new Order(1, new List<OrderItem>());
            Assert.Equal(0, order.TotalPrice);
        }

        [Fact]
        public void Order_Created()
        {
            IReadOnlyCollection<OrderItem> collection = new List<OrderItem>()
            {
                new OrderItem(1, 10, 10m),
                new OrderItem(2, 20, 20m)
            };

            Order order = new Order(1, collection);

            Assert.Equal(1, order.Id);
            //Assert.Collection(collection, item => Assert.Equal(1, item.BookId));
            //Assert.Collection(collection, item => Assert.Equal(2, item.BookId));
        }

        [Fact]
        public void Order_TotalCount()
        {
            IReadOnlyCollection<OrderItem> collection = new List<OrderItem>()
            {
                new OrderItem(1, 10, 10m),
                new OrderItem(2, 20, 20m)
            };
            Order order = new Order(1, collection);
            Assert.Equal(30, order.TotalCount);
        }

        [Fact]
        public void Order_Price()
        {
            IReadOnlyCollection<OrderItem> collection = new List<OrderItem>()
            {
                new OrderItem(1, 10, 10m),
                new OrderItem(2, 20, 20m)
            };
            Order order = new Order(1, collection);
            Assert.Equal(500, order.TotalPrice);
        }

        [Fact]
        public void Get_WithExistingItem_ReturnsItem()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1, 3, 10m),
                new OrderItem(2, 5, 100m)
            });

            OrderItem orderItem = order.Get(1);
            Assert.Equal(3, orderItem.Count);

            orderItem = order.Get(2);
            Assert.Equal(5, orderItem.Count);
        }

        [Fact]
        public void Get_ThrowException_Order_NotExist()
        {
            var order = new Order(1, new[]
{
                new OrderItem(1, 3, 10m),
                new OrderItem(2, 5, 100m)
            });

            Assert.Throws<InvalidOperationException>(() => order.Get(4));
        }
    }
}
