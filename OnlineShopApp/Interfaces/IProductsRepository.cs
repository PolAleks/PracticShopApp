using OnlineShopApp.Models;
using System.Security.Cryptography;

namespace OnlineShopApp.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product? TryGetById(int id);
        void Add(string name, decimal cost, string description);
    }
}
