namespace UserAppService.InputModel
{
    public class DeleteUserRolesInputModel : InputBaseModel
    {
        public int UserId { get; set; }

        /// <summary>
        /// This field would expect user roles to be specified in comma separated string
        /// </summary>
        public string RoleNames { get; set; }
    }
}
