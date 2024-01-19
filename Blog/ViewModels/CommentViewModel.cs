using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Comment")]
        public string Content { get; set; }
    }
}
