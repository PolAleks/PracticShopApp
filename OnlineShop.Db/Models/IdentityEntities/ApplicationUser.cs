using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
