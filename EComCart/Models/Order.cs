using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComCart.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}