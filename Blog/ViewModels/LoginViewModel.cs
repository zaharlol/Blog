using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name", Prompt = "Введите Имя")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string PasswordReg { get; set; }
    }
}
