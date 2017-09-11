using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class ConfirmEmailInputModel : InputBaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(this.Email, ResourceFiles.LocalizedText.RequireEmail);
            RequireInput(this.Code, ResourceFiles.LocalizedText.RequireConfirmEmailCode);
        }
    }
}
