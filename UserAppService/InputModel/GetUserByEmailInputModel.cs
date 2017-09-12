using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class GetUserByEmailInputModel : InputBaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(Email, ResourceFiles.LocalizedText.RequireEmail);
        }
    }
}
