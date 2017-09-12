using System;
using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class UserRegisterInputModel : InputBaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);

            if( Password != ConfirmPassword )
            {
                throw new Exception(ResourceFiles.LocalizedText.PasswordDoNotMatch);
            }
        }

    }
}
