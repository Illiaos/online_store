using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (items == null) throw new ArgumentNullException(nameof(items));
            Id = id;
            this.items = new List<OrderItem>(items);
        }

        public bool AddNewBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            int index = items.FindIndex(item => item.BookId == book.Id);
            if(index == -1)
            {
                items.Add(new OrderItem(book.Id, 1, book.Price));
            }
            return true;
        }

        public OrderItem Get(int bookId)
        {
            int index = items.FindIndex(item => item.BookId == bookId);
            if (index == -1)
                throw new InvalidOperationException("Book Not Found");
            return items[index];
        }

        public bool ContainsBook(int bookId)
        {
            return items.Any(item => item.BookId == bookId);
        }

        public void RemoveItem(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            int index = items.FindIndex(item => item.BookId == book.Id);
            if (index == -1) throw new InvalidOperationException();
            OrderItem orderItem = items[index];
            items.Remove(orderItem);
        }
    }
}
