﻿using Microsoft.AspNetCore.Identity;

namespace UserAppService.Models
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public UserLogin()
            : base()
        { }
    }
}