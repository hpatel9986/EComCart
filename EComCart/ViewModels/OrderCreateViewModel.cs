using Microsoft.AspNetCore.Mvc.Rendering;

namespace EComCart.ViewModels
{
    public class OrderCreateViewModel
    {
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<OrderItemLineViewModel> Items { get; set; } = new List<OrderItemLineViewModel>();
    }

    public class OrderItemLineViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public decimal UnitPrice { get; set; }

        public bool Selected { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
