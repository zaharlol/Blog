using Blog.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class UserViewModel
    {
        User User { get; set; }

        public UserViewModel(User user)
        {
            User = user;
        }

        public List<Article> Articles { get; set; }
    }
}
