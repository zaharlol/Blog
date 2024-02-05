using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Id_Tag {  get; set; }

        public User User { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
