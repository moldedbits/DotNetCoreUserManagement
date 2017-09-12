using Microsoft.AspNetCore.Identity;

namespace UserAppService.Models
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public UserClaim()
            : base()
        { }
    }
}