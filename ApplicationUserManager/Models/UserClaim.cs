using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAppService.Models
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public UserClaim()
            : base()
        { }


        //[Required]
        //public int UpdatedById { get; set; }

        //public DateTime UpdatedOn { get; set; }

        //[Required]
        //public int CreatedById { get; set; }

        //public DateTime? CreatedOn { get; set; }


        //[ForeignKey("UpdatedById")]
        //public virtual User UpdatedBy { get; set; }

        //[ForeignKey("CreatedById")]
        //public virtual User CreatedBy { get; set; }

    }
}