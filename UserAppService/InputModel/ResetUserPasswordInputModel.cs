using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class ResetUserPasswordInputModel : InputBaseModel
    {
        [Required]
        public string AlternateId { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(this.AlternateId, ResourceFiles.LocalizedText.RequireAlternateId);
            RequireInput(this.NewPassword, ResourceFiles.LocalizedText.RequirePassword);
        }
    }
}
