using System;

namespace Blog.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public Article Article { get; set; }

        public User User { get; set; }
    }
}
