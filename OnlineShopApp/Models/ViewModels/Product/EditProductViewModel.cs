using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models.ViewModels.Product
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название товара обязательно")]
        [Display(Name = "Название товара")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть от 0.01 до 1 000 000")]
        [Display(Name = "Цена, руб.")]
        [DataType(DataType.Currency)]
        public decimal Cost{ get; set; }

        [Required(ErrorMessage = "Описание обязательно")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
