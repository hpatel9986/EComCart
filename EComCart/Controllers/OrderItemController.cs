using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EComCart.Models;
using EComCart.ViewModels;

namespace EComCart.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly AppDbContext _db;

        public OrderItemController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .ToList();

            return View(items);
        }

        public IActionResult Add()
        {
            var vm = BuildVm(new OrderItem());
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(OrderItemFormViewModel vm)
        {
            vm = BuildVm(vm.OrderItem);

            if (!ModelState.IsValid)
                return View(vm);

            // calculate totals
            var product = _db.Products.Find(vm.OrderItem.ProductId);
            if (product == null)
            {
                ModelState.AddModelError("", "Invalid product.");
                return View(vm);
            }

            vm.OrderItem.UnitPrice = product.Price;
            vm.OrderItem.TotalPrice = product.Price * vm.OrderItem.Quantity;

            _db.OrderItems.Add(vm.OrderItem);
            _db.SaveChanges();

            // update order total
            RecalculateOrderTotal(vm.OrderItem.OrderId);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = _db.OrderItems.Find(id);
            if (item == null) return NotFound();

            var vm = BuildVm(item);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(OrderItemFormViewModel vm)
        {
            vm = BuildVm(vm.OrderItem);

            if (!ModelState.IsValid)
                return View(vm);

            var product = _db.Products.Find(vm.OrderItem.ProductId);
            if (product == null)
            {
                ModelState.AddModelError("", "Invalid product.");
                return View(vm);
            }

            vm.OrderItem.UnitPrice = product.Price;
            vm.OrderItem.TotalPrice = product.Price * vm.OrderItem.Quantity;

            _db.OrderItems.Update(vm.OrderItem);
            _db.SaveChanges();

            RecalculateOrderTotal(vm.OrderItem.OrderId);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var item = _db.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .FirstOrDefault(oi => oi.OrderItemId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = _db.OrderItems.Find(id);
            if (item == null) return NotFound();

            var orderId = item.OrderId;

            _db.OrderItems.Remove(item);
            _db.SaveChanges();

            RecalculateOrderTotal(orderId);

            return RedirectToAction("Index");
        }

        private OrderItemFormViewModel BuildVm(OrderItem item)
        {
            return new OrderItemFormViewModel
            {
                OrderItem = item,
                Orders = _db.Orders
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => new SelectListItem { Value = o.OrderId.ToString(), Text = $"Order #{o.OrderId}" })
                    .ToList(),
                Products = _db.Products
                    .OrderBy(p => p.Name)
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToList()
            };
        }

        private void RecalculateOrderTotal(int orderId)
        {
            var order = _db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null) return;

            order.TotalAmount = order.OrderItems.Sum(i => i.TotalPrice);
            _db.Orders.Update(order);
            _db.SaveChanges();
        }
    }
}
