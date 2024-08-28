using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable
namespace Store
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> items;
        public IReadOnlyCollection<OrderItem> Items 
        {
            get { return items; }
        }

        public int TotalCount
        {
            get
            {
                return items.Sum(item => item.Count);
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return items.Sum(item => item.Price * item.Count);
            }
        }

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if(items == null) throw new ArgumentNullException(nameof(items)); 
            Id = id;
            this.items = new List<OrderItem>(items);
        }
        
        public bool AddItem(Book book, int count)
        {
            if(book == null) throw new ArgumentNullException(nameof(book));
            OrderItem orderItem = items.SingleOrDefault(item => item.BookId == book.Id);
            if (orderItem == null)
            {
                items.Add(new OrderItem(book.Id, count, book.Price));
            }
            else
            {
                items.Add(new OrderItem(book.Id, count + orderItem.Count, book.Price));
                items.Remove(orderItem);
            }
            return true;
        }
    }
}
