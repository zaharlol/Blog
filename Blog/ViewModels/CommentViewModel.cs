using Blog.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        public List<Comment> Comments { get; set; }

        public Article Article { get; set; }

        public CommentViewModel() { }

        public CommentViewModel(string content, List<Comment> comments, Article article)
        {
            Content = content;
            Comments = comments;
            Article = article;
        }
    }
}
