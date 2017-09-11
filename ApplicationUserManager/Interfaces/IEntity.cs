using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserAppService.Models;

namespace UserAppService.Interfaces
{
    public interface IEntity
    {
        [Required]
        int UpdatedById { get; set; }

        DateTime UpdatedOn { get; set; }

        [Required]
        int CreatedById { get; set; }

        DateTime? CreatedOn { get; set; }

        [ForeignKey("UpdatedById")]
        User UpdatedBy { get; set; }

        [ForeignKey("CreatedById")]
        User CreatedBy { get; set; }
    }
}