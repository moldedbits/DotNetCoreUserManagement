using Microsoft.AspNetCore.Identity;
using System;
using UserAppService.Interfaces;

namespace UserAppService.Models
{
    public enum Role
    {
        SUPERADMIN = 1,
        ADMIN = 2,

    };

    public class ApplicationRole : IdentityRole<int>, IEntity
    {
        public ApplicationRole() : base() { }

        public string Description { get; set; }

        public ApplicationRole(string name, string description)
            : base()
        {
            Name = name;
            Description = description;
        }

        public int UpdatedById { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual User UpdatedBy { get; set; }

        public virtual User CreatedBy { get; set; }
    }
}