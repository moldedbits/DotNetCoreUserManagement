using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class ForgotPasswordInputModel : InputBaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
        }
    }
}
