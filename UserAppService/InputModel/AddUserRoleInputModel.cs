using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModel
{
    public class AddUserRoleInputModel : InputBaseModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string RoleName { get; set; }


    }
}
