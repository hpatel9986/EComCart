using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EComCart.Models;
using EComCart.ViewModels;

namespace EComCart.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _db;

        public OrderController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var orders = _db.Orders
                .Include(o => o.Customer)
                .ToList();

            return View(orders);
        }

        public IActionResult Add()
        {
            var vm = new OrderCreateViewModel
            {
                Customers = _db.Customers
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList(),

                Items = _db.Products
                    .OrderBy(p => p.Name)
                    .Select(p => new OrderItemLineViewModel
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        UnitPrice = p.Price,
                        Selected = false,
                        Quantity = 1
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(OrderCreateViewModel vm)
        {
            // repopulate dropdowns if we need to return the view
            vm.Customers = _db.Customers
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            if (vm.Items == null || vm.Items.Count == 0)
                vm.Items = new List<OrderItemLineViewModel>();

            // Basic validation: at least one item selected
            if (!vm.Items.Any(i => i.Selected))
            {
                ModelState.AddModelError("", "Please select at least one product.");
            }

            if (!ModelState.IsValid)
            {
                // rebuild product display values (name/price) because only ids/selected/qty post back reliably
                var productMap = _db.Products.ToDictionary(p => p.Id, p => p);
                foreach (var line in vm.Items)
                {
                    if (productMap.TryGetValue(line.ProductId, out var p))
                    {
                        line.ProductName = p.Name;
                        line.UnitPrice = p.Price;
                    }
                }

                return View(vm);
            }

            var order = new Order
            {
                CustomerId = vm.CustomerId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            foreach (var line in vm.Items.Where(i => i.Selected))
            {
                var product = _db.Products.Find(line.ProductId);
                if (product == null) continue;

                var qty = line.Quantity <= 0 ? 1 : line.Quantity;

                var item = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = qty,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * qty
                };

                total += item.TotalPrice;
                order.OrderItems.Add(item);
            }

            order.TotalAmount = total;

            _db.Orders.Add(order);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // EDIT (header-level: customer + order date)
        public IActionResult Edit(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return NotFound();

            ViewBag.Customers = new SelectList(_db.Customers.OrderBy(c => c.Name).ToList(), "Id", "Name", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            ViewBag.Customers = new SelectList(_db.Customers.OrderBy(c => c.Name).ToList(), "Id", "Name", order.CustomerId);

            if (!ModelState.IsValid)
                return View(order);

            // Keep totals/items unchanged here; line item edits are handled in OrderItem CRUD.
            _db.Orders.Update(order);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var order = _db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();

            _db.OrderItems.RemoveRange(order.OrderItems);
            _db.Orders.Remove(order);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
