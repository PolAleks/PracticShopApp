using Microsoft.AspNetCore.Http;
using OnlineShop.Core.Interfaces.Services;

namespace OnlineShop.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
    {
        public string UserName => accessor.HttpContext.User.Identity?.Name ?? "anonymous";
    }
}
