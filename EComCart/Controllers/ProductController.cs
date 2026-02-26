using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext db = new AppDbContext();

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Add()
        {
            ViewBag.CategoryList =
                new SelectList(db.Categories.ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var product = db.Products.Find(id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}