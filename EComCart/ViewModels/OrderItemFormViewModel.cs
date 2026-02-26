using Microsoft.AspNetCore.Mvc.Rendering;
using EComCart.Models;

namespace EComCart.ViewModels
{
    public class OrderItemFormViewModel
    {
        public OrderItem OrderItem { get; set; } = new OrderItem();

        public IEnumerable<SelectListItem> Orders { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<SelectListItem> Products { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
