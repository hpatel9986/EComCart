using System.ComponentModel.DataAnnotations;

namespace EComCart.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(80)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Product>? Products { get; set; }
    }
}