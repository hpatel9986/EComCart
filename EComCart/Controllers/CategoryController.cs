using Microsoft.AspNetCore.Mvc;
using EComCart.Models;

namespace EComCart.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext db = new AppDbContext();

        // SHOW ALL
        public IActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        // OPEN ADD FORM
        public IActionResult Add()
        {
            return View();
        }

        // SAVE CATEGORY
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

        // OPEN EDIT
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            return View(category);
        }

        // SAVE EDIT
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}