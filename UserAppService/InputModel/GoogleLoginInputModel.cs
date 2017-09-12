using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class GoogleLoginInputModel : InputBaseModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Token { get; set; }

        public bool AutoRegister { get; set; } = true;
    }
}
