using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models
{
    public class Registration
    {
        [Display(Name = "Логин", Prompt = "Ваш логин")]
        [Required(ErrorMessage = "Не указан логин")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "В качестве логина используйте email адрес")]
        [StringLength(30, ErrorMessage = "Логин должен быть от {2} до {1} символов", MinimumLength = 5)]
        public string Login { get; set; } = null!;

        [Display(Name = "Пароль", Prompt = "Ваш пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(60, ErrorMessage = "Пароль должен быть от {2} до {1} символов", MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Display(Name = "Подтвердите пароль", Prompt = "Подтвердите пароль")]
        [Required(ErrorMessage = "Не указан повторный пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
