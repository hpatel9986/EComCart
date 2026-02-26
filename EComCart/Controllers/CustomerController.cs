using Microsoft.AspNetCore.Mvc;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _db;

        public CustomerController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Customers.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);

            _db.Customers.Remove(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }


        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Update(customer);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }
    }
}