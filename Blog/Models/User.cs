using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordReg { get; set; }
        public int Id_Article { get; set; }
    }
}
