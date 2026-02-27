using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
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
            var builder = WebApplication.CreateBuilder(args);

            // Получение строки соединения с БД из appsettings.json 
            string connection = builder.Configuration.GetConnectionString("OnlineShopConnection");
            // Добавление в контейнер зависимостей DatabaseContext для работы с БД
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connection));

            builder.Host.UseSerilog((context, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.WithProperty("ApplicationName", "Online Shop"));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<ICartsRepository, InMemoryCartsRepository>();

            //builder.Services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
            builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();

            builder.Services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
            builder.Services.AddSingleton<IFavoritesRepository, InMemoryFavoritesRepository>();
            builder.Services.AddSingleton<IComparisonRepository, InMemoryComparisonsRepository>();
            builder.Services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();
            builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();

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
