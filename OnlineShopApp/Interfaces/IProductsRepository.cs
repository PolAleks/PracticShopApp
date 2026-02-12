using OnlineShopApp.Models;
using OnlineShopApp.Models.ViewModels.Product;
using System.Security.Cryptography;

namespace OnlineShopApp.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product? TryGetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        List<Product> Search(string query);
    }
}
