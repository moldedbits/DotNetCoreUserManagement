using Microsoft.AspNetCore.Identity;

namespace UserAppService.Models
{
    public class UserToken : IdentityUserToken<int>//, IEntity
    {
        public UserToken()
            : base()
        { }
    }
}
