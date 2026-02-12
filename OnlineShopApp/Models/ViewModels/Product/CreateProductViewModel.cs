using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Название товара обязательно")]
        [Display(Name = "Название товара", Prompt = "Наименование товара")]        
        public string Name { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть от 0.01 до 1 000 000")]
        [Display(Name = "Цена, руб.", Prompt = "Цена, руб.")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Описание обязательно")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание", Prompt = "Описание товара")]
        public string Description { get; set; }
    }
}
