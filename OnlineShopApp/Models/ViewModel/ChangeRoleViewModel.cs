using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShopApp.Models.ViewModel
{
    public class ChangeRoleViewModel
    {
        [Display(Name = "Логин")]
        [AllowNull]
        public Guid Id { get; set; }

        [Display(Name = "Роль")]
        [AllowNull]
        public string Role { get; set; }

        [AllowNull]
        public List<SelectListItem> Roles { get; set; }
    }
}
