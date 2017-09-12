using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class FacebookLoginInputModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string AppId { get; set; }

        public bool AutoRegister { get; set; } = true;

        [Required]
        public string Email { get; set; }
    }
}
