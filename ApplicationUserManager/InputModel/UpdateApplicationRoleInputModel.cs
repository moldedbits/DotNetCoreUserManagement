using System.ComponentModel.DataAnnotations;
using UserAppService.Models;

namespace UserAppService.InputModel
{
    public class UpdateApplicationRoleInputModel : InputBaseModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(Id, ResourceFiles.LocalizedText.RequireId);
            RequireInput(Name, ResourceFiles.LocalizedText.RequireName);
        }
    }
}
