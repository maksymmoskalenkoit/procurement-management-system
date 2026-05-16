using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationForEnterprise.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата замовлення")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Статус")]
        public string Status { get; set; } = "Очікується";

        [Display(Name = "Постачальник")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
    }
}