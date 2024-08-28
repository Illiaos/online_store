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

        public IActionResult AddItem(int id)
        {
            Cart cart;

            Order order;
            if (HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }

            var book = bookRepository.GetById(id);
            order.AddItem(book, 1);
            orderRepository.Update(order);
            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);
            return RedirectToAction("Index", "Book", new { id = id });
        }
    }
}
