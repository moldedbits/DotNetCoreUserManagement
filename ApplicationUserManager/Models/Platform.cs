using System;
using System.ComponentModel.DataAnnotations;
using UserAppService.Interfaces;

namespace UserAppService.Models
{
    public class Platform : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public int UpdatedById { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual User UpdatedBy { get; set; }

        public virtual User CreatedBy { get; set; }
    }
}