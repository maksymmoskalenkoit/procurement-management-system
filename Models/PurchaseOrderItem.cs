using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationForEnterprise.Models
{
    public class PurchaseOrderItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Закупівля")]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder? PurchaseOrder { get; set; }

        [Display(Name = "Товар")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Range(1, 1000000,
            ErrorMessage = "Кількість повинна бути мінімум 1")]
        [Display(Name = "Кількість")]
        public int Quantity { get; set; }

        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
    }
}