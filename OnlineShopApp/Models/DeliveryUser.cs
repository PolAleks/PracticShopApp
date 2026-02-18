using OnlineShopApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models
{
    public class DeliveryUser
    {
        public Guid Id { get; set; }

        [Display(Name = "Имя покупателя", Prompt = "Ваше имя")]
        [Required(ErrorMessage = "Не указано имя покупателя")]
        [DataType(DataType.Text)]
        [StringLength(25, ErrorMessage = "Имя должно содержать от {2} до {1} символов", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Адрес доставки", Prompt = "Ваш адрес")]
        [Required(ErrorMessage = "Не указан адрес доставки")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Адрес должен содержать от {2} до {1} символов", MinimumLength = 5)]
        public string Address { get; set; }

        [Display(Name = "Телефон", Prompt = "Ваш телефон")]
        [Required(ErrorMessage = "Не указан телефон покупателя")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Телефон может содержать только цифры")]
        [StringLength(16, ErrorMessage = "Телефон должен содержать от {2} до {1} цифр", MinimumLength = 5)]
        public string Phone { get; set; }

        [Display(Name = "Дата доставки", Prompt = "дд.мм.гггг")]
        [Required(ErrorMessage = "Не указана дата доставки")]
        [DateRange]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [Display(Name = "Комментарий", Prompt = "Ваш комментарий")]
        [MaxLength(512, ErrorMessage = "Комментарий не должен превышать {1} символов")]
        [DataType(DataType.MultilineText)]
        public string? Comment { get; set; }
    }
}
