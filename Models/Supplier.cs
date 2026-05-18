using System.ComponentModel.DataAnnotations;

namespace WebApplicationForEnterprise.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Назва постачальника")]
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
    }
}