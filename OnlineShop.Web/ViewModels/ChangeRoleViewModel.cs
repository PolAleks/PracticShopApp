using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShop.Web.ViewModels
{
    public class ChangeRoleViewModel
    {
        [AllowNull]
        public string Id { get; set; }

        [Display(Name = "Роль")]
        [AllowNull]
        public string Role { get; set; }

        [AllowNull]
        public List<SelectListItem> Roles { get; set; }
    }
}
