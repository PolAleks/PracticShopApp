using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product? TryGetById(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> Search(string query);
    }
}
