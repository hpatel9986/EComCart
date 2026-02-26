using EComCart.Models;
using Microsoft.AspNetCore.Mvc;

namespace EComCart.Controllers
{
    public class CustomerController : Controller
    {
        // create DbContext object
        AppDbContext db = new AppDbContext();

        // SHOW ALL CUSTOMERS
        public IActionResult Index()
        {
            var customers = db.Customers.ToList();
            return View(customers);
        }

        // OPEN ADD FORM
        public IActionResult Add()
        {
            return View();
        }

        // SAVE CUSTOMER
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // OPEN EDIT FORM
        public IActionResult Edit(int id)
        {
            var customer = db.Customers.Find(id);
            return View(customer);
        }

        // SAVE EDIT
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            db.Customers.Update(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // SHOW DETAILS
        public IActionResult Details(int id)
        {
            var customer = db.Customers.Find(id);
            return View(customer);
        }

        // DELETE CUSTOMER
        public IActionResult Delete(int id)
        {
            var customer = db.Customers.Find(id);

            db.Customers.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}