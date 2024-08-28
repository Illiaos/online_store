using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests
{
    public class OrderItemTest
    {
        [Fact]
        public void OrderItem_WithZero_ThrowArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(1, 0, 10m));
        }

        [Fact]
        public void OrderItem_WithNegative_ThrowArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(1, -1, 10m));
        }

        [Fact]
        public void OrderItem_WithPositive_ThrowArgumentException()
        {
            OrderItem orderItem = new OrderItem(1, 10, 10m);
            Assert.Equal(1, orderItem.BookId);
            Assert.Equal(10, orderItem.Count);
            Assert.Equal(10m, orderItem.Price);
        }
    }
}
