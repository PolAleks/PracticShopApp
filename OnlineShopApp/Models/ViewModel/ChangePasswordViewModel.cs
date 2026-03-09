using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShopApp.Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Пароль", Prompt = "Ваш пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть от {2} до {1} символов")]
        [AllowNull]
        public string Password { get; set; }

        [Display(Name = "Подтвердите пароль", Prompt = "Подтвердите пароль")]
        [Required(ErrorMessage = "Не указан повторный пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [AllowNull]
        public string ConfirmPassword { get; set; }
    }
}
