using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Mappings;
using OnlineShop.Infrastructure.Services;
using OnlineShop.Web.Mappings;
using Serilog;
using System.Globalization;

namespace OnlineShop.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Установка глобального поведения для всех DateTime
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            // Получение строки соединения с БД из appsettings.json 
            string connection = builder.Configuration.GetConnectionString("OnlineShopConnection")
                ?? throw new InvalidOperationException("Строка подключения 'OnlineShopConnection' не найдена");

            // Добавление в контейнер зависимостей DatabaseContext для работы с БД
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connection));

            builder.Host.UseSerilog((context, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.WithProperty("ApplicationName", "Online Shop"));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(cfg => { }, typeof(WebMappingProfile).Assembly, typeof(InfrastructureMappingProfile).Assembly);

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IRoleService, RoleService>();

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ICartService, CartService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IComparisonService, ComparisonService>();
            builder.Services.AddTransient<IFavoriteService, FavoriteService>();

            // Добавления в Ioc контейнер сервис аутентификации и настраиваем его
            // Указываем модели которые содержат пользователей и роли
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 3;
            })
                // Добавили провайдер токенов по умолчанию
                .AddDefaultTokenProviders()
                // Руссификация ошибок IdentityError
                .AddRussianIdentityErrorDescriber()
                // Указываем какой именно контекст БД использовать для работы с наборами пользователей и ролей!
                .AddEntityFrameworkStores<DatabaseContext>()
                // Настройка и создание автоматического хранилища пользователя(репозитория)
                .AddUserStore<UserStore<User, IdentityRole, DatabaseContext, string>>()
                // Настройка хранилища для ролей
                .AddRoleStore<RoleStore<IdentityRole, DatabaseContext, string>>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/Account/Authorization"; // Сылка для авторизации, если нет доступа
                options.LogoutPath = "/Account/Logout"; // Сылка на метод выхода
                options.Cookie = new CookieBuilder() { IsEssential = true }; // В каждом запросе должны быть cookies
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru-RU")
                };
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
            builder.Services.AddTransient<IAuthService, AuthService>();

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                // Применяем миграции если они есть, до запуска приложения
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();

                // Добавляем роли у учетную запись администратора, если их нет
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await IdentityInit.IntitFirstData(userManager, roleManager);
            }

            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
