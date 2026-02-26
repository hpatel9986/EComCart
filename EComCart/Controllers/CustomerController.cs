using Microsoft.AspNetCore.Mvc;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class CustomerController : Controller
    {
        AppDbContext db = new AppDbContext();

        public IActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var customer = db.Customers.Find(id);

            db.Customers.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var customer = db.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var customer = db.Customers.Find(id);

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
                db.Customers.Update(customer);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }
    }
}