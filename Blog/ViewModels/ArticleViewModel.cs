using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class ArticleViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display (Name = "title", Prompt = "Заголовок")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "title", Prompt = "Содержание")]
        public string Content { get; set; }
    }
}
