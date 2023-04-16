using Entities.Enums;
using Microsoft.AspNetCore.Identity;


namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
        public string Cellphone { get; set; }
        public EUserType UserType { get; set; }
    }
}
