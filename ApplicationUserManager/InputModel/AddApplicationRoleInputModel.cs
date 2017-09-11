using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class AddApplicationRoleInputModel : InputBaseModel
    {
        /// <summary>
        /// Role Name (unique field as every role has unique name)
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the role
        /// </summary>
        public string Description { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(Name, ResourceFiles.LocalizedText.RequireName);
        }
    }
}
