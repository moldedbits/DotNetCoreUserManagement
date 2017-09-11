using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAppService.InputModel
{
    public class GetUserByUserNameInputModel : InputBaseModel
    {
        [Required]
        public string UserName { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(UserName, ResourceFiles.LocalizedText.RequireUserName);
        }
    }
}
