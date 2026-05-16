using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationForEnterprise.Models
{
    public class CustomerOrder
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата замовлення")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Статус")]
        public string Status { get; set; } = "Створено";

        [Display(Name = "Клієнт")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; }
            = new List<CustomerOrderItem>();
    }
}