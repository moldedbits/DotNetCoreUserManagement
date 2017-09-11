using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAppService.Models
{
    public class IdentityMessageExtended : IdentityMessage
    {
        public string Html { get; set; }
    }
}
