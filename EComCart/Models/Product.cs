using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComCart.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(80)]
        public string Name { get; set; }

        // Fix decimal precision warning
        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}