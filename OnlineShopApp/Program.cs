using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Migrations;
using OnlineShop.Db.Models.IdentityEntities;
using OnlineShop.Db.Repositories;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Repositories;
using OnlineShopApp.Services;
using Serilog;
using System.Globalization;
using System.Runtime;

namespace OnlineShopApp
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

            builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
            builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
            builder.Services.AddTransient<IFavoritesRepository, FavoritesDbRepository>();
            builder.Services.AddTransient<IComparisonRepository, ComparisonsDbRepository>();
            builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();

            // Добавления в Ioc контейнер сервис аутентификации и настраиваем его
            // Указываем модели которые содержат пользователей и роли
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
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
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, DatabaseContext, Guid>>()
                // Настройка хранилища для ролей
                .AddRoleStore<RoleStore<ApplicationRole, DatabaseContext, Guid>>();

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

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                // Применяем миграции если они есть, до запуска приложения
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();

                // Добавляем роли у учетную запись администратора, если их нет
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

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
