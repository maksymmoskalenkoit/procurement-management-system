using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationForEnterprise.Models
{
    public class CustomerOrderItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Замовлення")]
        public int CustomerOrderId { get; set; }

        [ForeignKey("CustomerOrderId")]
        public CustomerOrder? CustomerOrder { get; set; }

        [Display(Name = "Товар")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Display(Name = "Кількість")]
        public int Quantity { get; set; }

        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
    }
}