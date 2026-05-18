using System.ComponentModel.DataAnnotations;

namespace WebApplicationForEnterprise.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Назва клієнта")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Адреса")]
        public string? Address { get; set; }

        public ICollection<CustomerOrder> CustomerOrders { get; set; }
            = new List<CustomerOrder>();
    }
}