using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.ViewModels
{
    public class CreateProductViewModel
    {
        [Display(Name = "Наименование товара", Prompt = "Наименование товара")]
        [Required(ErrorMessage = "Не указано наименование товара")]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Наименование товара должно содержать от {2} до {1} символов", MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Цена, руб.", Prompt = "Цена, руб.")]
        [Required(ErrorMessage = "Не указана цена товара")]
        [Range(0, 1000000, ErrorMessage = "Стоимость должна быть в диапазоне от {1} до {2:c}")]
        public decimal Cost { get; set; }

        [Display(Name = "Описание товара", Prompt = "Описание товара")]
        [MaxLength(4069, ErrorMessage = "Описание товара не должно превышать {1} символов")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Изображения товара")]
        public List<IFormFile> Images { get; set; } = [];
    }
}
