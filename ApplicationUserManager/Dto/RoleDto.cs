using System;
using UserAppService.Models;

namespace UserAppService.Dto
{
    public class RoleDto
    {
        //
        // Summary:
        //     Role id
        public int Id { get; set; }
        //
        // Summary:
        //     Role name
        public string Name { get; set; }

        public string Description { get; set; }

        public int UpdatedById { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public User UpdatedBy { get; set; }

        public User CreatedBy { get; set; }
    }
}
