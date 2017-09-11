namespace UserAppService.InputModel
{
    public class DeleteUserRoleInputModel : InputBaseModel
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}
