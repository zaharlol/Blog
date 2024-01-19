using Microsoft.AspNetCore.Identity;
using System;

namespace Blog.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordReg { get; set; }
        public int Id_Article { get; set; }
    }
}
