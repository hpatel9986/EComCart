using Microsoft.AspNetCore.Mvc.Rendering;
using EComCart.Models;

namespace EComCart.ViewModels
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; } = new Product();

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
