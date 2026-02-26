using System.ComponentModel.DataAnnotations;

namespace EComCart.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(80)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        // automatically saves current date
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
