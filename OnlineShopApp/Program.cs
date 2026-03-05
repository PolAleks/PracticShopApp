using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models.IdentityEntities;
using OnlineShop.Db.Repositories;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Repositories;
using Serilog;
using System.Globalization;

namespace OnlineShopApp
{
    public class Program
    {
        public static void Main(string[] args)
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

            builder.Services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();
            builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();

            // Добавления в Ioc контейнер сервис аутентификации и настраиваем его
            // Указываем модели которые содержат пользователей и роли
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
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


            // Применяем миграции если они есть, до запуска приложения
            using(var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();
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
