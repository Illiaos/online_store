using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;
using System.Security.Permissions;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository)
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                Order order = orderRepository.GetById(cart.OrderId);
                OrderModel model = new OrderModel();

                model.Items = new OrderItemModel[order.Items.Count];

                var bookIds = order.Items.Select(item => item.BookId).ToArray();
                Book[] books = bookRepository.GetAllByIds(bookIds);

                for (int i = 0; i != order.Items.Count; ++i)
                {
                    int bookId = order.Items.ElementAt(i).BookId;
                    Book book = books.Single(item => item.Id == bookId);
                    
                    model.Items[i] = new OrderItemModel()
                    {
                        BookId = bookId,
                        Author = book.Author,
                        Title = book.Title,
                        Count = order.Items.ElementAt(i).Count,
                        Price = order.Items.ElementAt(i).Price,
                    };
                }

                model.TotalPrice = order.TotalPrice;
                model.TotalCount = order.TotalCount;
                return View(model);
            }
            return View("Empty");
        }


        [HttpPost]
        public IActionResult EditBook(int bookId, int count)
        {
            GetOrCreateOrderAndCart(out Order order, out Cart cart);
            order.Get(bookId).Count += count;
            orderRepository.Update(order);
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult AddItem(int bookId, int count)
        {
            GetOrCreateOrderAndCart(out Order order, out Cart cart);
            Book book = bookRepository.GetById(bookId);
            if (order.ContainsBook(bookId) == true)
            {
                order.Get(bookId).Count += count;
            }
            else
            {
                order.AddNewBook(book);
            }
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Book", new { id = bookId });
        }

        public IActionResult RemoveItem(int bookId)
        {
            GetOrCreateOrderAndCart(out Order order, out Cart cart);
            Book book = bookRepository.GetById(bookId);
            order.RemoveItem(book);
            orderRepository.Update(order);
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Order");
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);
        }

        private void GetOrCreateOrderAndCart(out Order order, out Cart cart)
        {
            if (HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }
        }
    }
}
