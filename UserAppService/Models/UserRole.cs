using Microsoft.AspNetCore.Identity;
using System;
using UserAppService.Interfaces;

namespace UserAppService.Models
{
    public class UserRole : IdentityUserRole<int>//, IEntity
    {
        public UserRole()
            : base()
        { }
    }
}