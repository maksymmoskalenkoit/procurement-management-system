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

        [Range(1, 1000000,
            ErrorMessage = "Закупівельна ціна повинна бути більше 0")]
        [Display(Name = "Закупівельна ціна")]
        public decimal PurchasePrice { get; set; }


        [Range(1, 1000000,
            ErrorMessage = "Ціна продажу повинна бути більше 0")]
        [Display(Name = "Ціна продажу")]
        public decimal SellingPrice { get; set; }

        [Range(0, 1000000,
            ErrorMessage = "Кількість на складі не може бути від'ємною")]
        [Display(Name = "Кількість на складі")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Опис")]
        public string? Description { get; set; }
    }
}
