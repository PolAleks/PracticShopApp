using OnlineShopApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Логин", Prompt = "Логин пользователя")]
        [Required(ErrorMessage = "Не указан логин пользователя")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "В качестве логина используйте email адрес")]
        [StringLength(30, ErrorMessage = "Логин должен быть от {2} до {1} символов", MinimumLength = 5)]
        public string? Login { get; set; }

        [Display(Name = "Пароль", Prompt = "Пароль пользователя")]
        [Required(ErrorMessage = "Не указан пароль пользователя")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Пароль должен быть от {2} до {1} символов", MinimumLength = 6)]
        public string? Password { get; set; }

        [PhoneNumber]
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Display(Name = "Телефон", Prompt = "+7(XXX)XXX-XX-XX")]
        public string? Phone { get; set; }

        [Display(Name = "Имя", Prompt = "Введите имя")]
        [Required(ErrorMessage = "Не указано имя пользователя")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно быть от {2} до {1} символов")]
        public string? FirstName { get; set; }

        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        [Required(ErrorMessage = "Не указано фамилия пользователя")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от {2} до {1} символов")]
        public string? LastName { get; set; }

        public string? Role { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
