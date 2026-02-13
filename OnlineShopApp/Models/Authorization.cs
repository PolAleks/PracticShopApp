using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models
{
    public class Authorization
    {
        [Display(Name = "Логин", Prompt = "Ваш логин")]
        [Required(ErrorMessage = "Не указан логин")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "В качестве логина используйте email")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Логин может содержать от {2} до {1} символов")]
        public string Login { get; set; } = null!;

        [Display(Name = "Пароль", Prompt = "Ваш пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(60, ErrorMessage = "Пароль должен содержать от {2} до {1} символов", MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Display(Name = "Запонить меня")]
        public bool IsRememberMe { get; set; }
    }
}
