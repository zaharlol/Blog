using Blog.Models;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class AccountViewModel
    {
        public string Name { get; set; }
        
        public List<Article> Articles { get; set; }
    }
}
