using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models
{
    public class Role
    {
        public Guid Id { get; set; }

        [Display(Name = "Роль", Prompt = "Введите новую роль")]
        [Required(ErrorMessage = "Не указано название роли")]
        [StringLength(50, ErrorMessage = "Роль должна содержать от {2} до {1} символов", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Name { get; set; } = null!;
    }
}
