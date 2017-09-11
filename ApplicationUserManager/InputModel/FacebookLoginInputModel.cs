using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAppService.InputModel
{
    public class FacebookLoginInputModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string AppId { get; set; }

        public bool AutoRegister { get; set; } = true;

        [Required]
        public string Email { get; set; }
    }
}
