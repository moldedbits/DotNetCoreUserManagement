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

    public class ApplicationRole : IdentityRole<int>//, IEntity
    {
        public ApplicationRole() : base() { }

        public string Description { get; set; }

        public ApplicationRole(string name, string description)
            : base()
        {
            Name = name;
            Description = description;
        }
    }
}