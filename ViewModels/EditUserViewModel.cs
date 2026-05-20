using System.ComponentModel.DataAnnotations;

namespace WebApplicationForEnterprise.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}