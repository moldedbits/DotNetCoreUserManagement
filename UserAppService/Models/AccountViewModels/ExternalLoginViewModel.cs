using System.ComponentModel.DataAnnotations;

namespace UserAppService.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
