using System.ComponentModel.DataAnnotations;

namespace EComCart.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public List<Order>? Orders { get; set; }
    }
}