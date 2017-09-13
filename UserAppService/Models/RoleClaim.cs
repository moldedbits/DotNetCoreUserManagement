using Microsoft.AspNetCore.Identity;

namespace UserAppService.Models
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public RoleClaim()
            : base()
        { }
    }
}