using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public static class InitialDataConfiguration
    {
        public static void FillData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Товар 1", Cost = 1500, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 2, Name = "Товар 2", Cost = 2000, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 3, Name = "Товар 3", Cost = 1300, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 4, Name = "Товар 4", Cost = 3000, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 5, Name = "Товар 5", Cost = 1400, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 6, Name = "Товар 6", Cost = 3060, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 7, Name = "Товар 7", Cost = 2800, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" },
                new Product() { Id = 8, Name = "Товар 7", Cost = 500, Description = "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", PhotoPath = "/img/product.png" }
                );
        }
    }
}