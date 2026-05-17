using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationForEnterprise.Enums;

namespace WebApplicationForEnterprise.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата замовлення")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Статус")]
        public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Очікується;

        [Display(Name = "Постачальник")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
           = new List<PurchaseOrderItem>();
    }
}