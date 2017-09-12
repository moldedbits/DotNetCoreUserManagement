using Microsoft.AspNetCore.Identity;
using System;
using UserAppService.Interfaces;

namespace UserAppService.Models
{
    public class UserRole : IdentityUserRole<int>, IEntity
    {
        public UserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }

        public User User { get; set; }

        public int UpdatedById { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual User UpdatedBy { get; set; }

        public virtual User CreatedBy { get; set; }
    }
}