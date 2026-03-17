using OnlineShop.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.ViewModels
{
    public class EditUserViewModel
    {
        public required string Id { get; set; }

        [Display(Name = "Логин")]
        public string? Login { get; set; }

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

        [PhoneNumber]
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Display(Name = "Телефон", Prompt = "+7(XXX)XXX-XX-XX")]
        public string? PhoneNumber { get; set; }

        
    }
}
