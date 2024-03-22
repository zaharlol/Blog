using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Data;

namespace API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public Role Role { get; set; }
    }
}
