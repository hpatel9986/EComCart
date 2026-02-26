using Microsoft.AspNetCore.Mvc;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext db = new AppDbContext();

        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

    }
}