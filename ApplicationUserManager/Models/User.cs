using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using UserAppService.Interfaces;

namespace UserAppService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IEntity
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Id", this.Id.ToString()));
            userIdentity.AddClaim(new Claim("UserName", this.UserName));
            userIdentity.AddClaim(new Claim("Email", this.Email));

            return userIdentity;
        }
            
        public ICollection<UserRole> UserRoles { get; set; }

        public string AlternateId { get; set; } = Guid.NewGuid().ToString("N");

        public string ResetToken { get; set; }

        [Required]
        public int UpdatedById { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        [NotMapped]
        public User UpdatedBy { get; set; }

        [NotMapped]
        public User CreatedBy { get; set; }
    }
}