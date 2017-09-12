using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class GetApplicationRoleByIdInputModel : InputBaseModel
    {
        [Required]
        public int Id { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(Id, ResourceFiles.LocalizedText.RequireId);
        }
    }
}
