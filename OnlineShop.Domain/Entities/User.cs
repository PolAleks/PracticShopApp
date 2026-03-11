using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Domain.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
}
