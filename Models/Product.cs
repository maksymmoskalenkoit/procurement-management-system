using System.ComponentModel.DataAnnotations;

namespace WebApplicationForEnterprise.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Назва товару")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Закупівельна ціна")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Ціна продажу")]
        public decimal SellingPrice { get; set; }

        [Display(Name = "Кількість на складі")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Опис")]
        public string? Description { get; set; }
    }
}
