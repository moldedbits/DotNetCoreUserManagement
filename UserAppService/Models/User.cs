using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserAppService.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace UserAppService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }

        public string AlternateId { get; set; } = Guid.NewGuid().ToString("N");

        public string ResetToken { get; set; }
    }
}