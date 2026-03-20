using Microsoft.AspNetCore.Http;
using OnlineShop.Core.Interfaces.Services;

namespace OnlineShop.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
    {
        public string UserName
        {
            get
            {
                var httpContext = accessor.HttpContext;

                if (httpContext == null)
                    return "anonymous";

                // Для авторизованных пользователей
                if(httpContext.User.Identity?.IsAuthenticated == true)
                {
                    return httpContext.User.Identity.Name ?? "anonymous";
                }

                // Для неавторизованных пользователей
                return GetOrCreateAnonymousName(httpContext);
            }
        }

        private static string GetOrCreateAnonymousName(HttpContext httpContext)
        {
            const string coockieName = "AnonymousName";

            if(httpContext.Request.Cookies.TryGetValue(coockieName, out var anonimousName))
            {
                return anonimousName;
            }

            // Уникальный идентификатор анонинмного пользователя
            anonimousName = $"anonymous_{Guid.NewGuid()}";

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(15),
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            };

            // Сохраняем идентификатор анонимного пользователя в cookies браузера клиента
            httpContext.Response.Cookies.Append(coockieName, anonimousName, cookieOptions);

            return anonimousName;
        }
    }
}