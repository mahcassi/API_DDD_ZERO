using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    internal class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
        public string Cellphone { get; set; }
        public int UserType { get; set; }
    }
}
