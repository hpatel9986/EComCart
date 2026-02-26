using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext db = new AppDbContext();

        // INDEX
        public IActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        // ADD GET
        public IActionResult Add()
        {
            ViewBag.CategoryList =
                new SelectList(db.Categories.ToList(), "Id", "Name");

            return View();
        }

        // ADD POST
        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryList =
                new SelectList(db.Categories.ToList(), "Id", "Name");

            return View(product);
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
                return NotFound();

            ViewBag.CategoryList =
                new SelectList(db.Categories.ToList(), "Id", "Name");

            return View(product);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Update(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryList =
                new SelectList(db.Categories.ToList(), "Id", "Name");

            return View(product);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}