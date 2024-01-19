using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class TagViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Tag")]
        public string Name { get; set; }
    }
}
