using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class OrderController : Controller
    {
        AppDbContext db = new AppDbContext();

        public IActionResult Index()
        {
            var orders =
                db.Orders.Include(o => o.Customer).ToList();

            return View(orders);
        }

        public IActionResult Add()
        {
            ViewBag.Customers = db.Customers.ToList();

            ViewBag.Products = db.Products.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(int CustomerId,
            List<int> ProductId,
            List<int> Quantity)
        {
            Order order = new Order();

            order.CustomerId = CustomerId;

            order.OrderItems = new List<OrderItem>();

            decimal total = 0;

            for (int i = 0; i < ProductId.Count; i++)
            {
                var product =
                    db.Products.Find(ProductId[i]);

                OrderItem item = new OrderItem();

                item.ProductId = product.Id;

                item.Quantity = Quantity[i];

                item.UnitPrice = product.Price;

                item.TotalPrice =
                    product.Price * Quantity[i];

                total += item.TotalPrice;

                order.OrderItems.Add(item);
            }

            order.TotalAmount = total;

            db.Orders.Add(order);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var order =
                db.Orders.Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == id);

            db.OrderItems.RemoveRange(order.OrderItems);

            db.Orders.Remove(order);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}