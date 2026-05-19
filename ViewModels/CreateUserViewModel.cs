using System.ComponentModel.DataAnnotations;

namespace WebApplicationForEnterprise.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; } = string.Empty;
    }
}