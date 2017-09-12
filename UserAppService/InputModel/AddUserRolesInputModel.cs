namespace UserAppService.InputModel
{
    public class AddUserRolesInputModel : InputBaseModel
    {
        public int UserId { get; set; }

        /// <summary>
        /// This field will have multiple roles in comma separated string that can be assigned to the user 
        /// </summary>
        public string RoleNames { get; set; }
    }
}
