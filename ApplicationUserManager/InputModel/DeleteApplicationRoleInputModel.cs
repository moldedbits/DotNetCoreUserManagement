using System.ComponentModel.DataAnnotations;
using UserAppService.Models;

namespace UserAppService.InputModel
{
    public class DeleteApplicationRoleInputModel : InputBaseModel
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
