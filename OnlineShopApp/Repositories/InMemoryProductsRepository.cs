using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;
using OnlineShopApp.Models.ViewModels.Product;

namespace OnlineShopApp.Repositories
{
    public class InMemoryProductsRepository : IProductsRepository
    {
        private int _instanceCounter = 0;
        private readonly List<Product> _products;

        public InMemoryProductsRepository()
        {
            _products = [
                new Product(++_instanceCounter, "Товар 1", 1000, "Описание 1"),
                new Product(++_instanceCounter, "Товар 2", 2000, "Описание 2"),
                new Product(++_instanceCounter, "Товар 3", 1500, "Описание 3"),
                new Product(++_instanceCounter, "Товар 4", 1700, "Описание 4"),
                new Product(++_instanceCounter, "Товар 5", 1250, "Описание 5"),
                new Product(++_instanceCounter, "Товар 6", 3500, "Описание 6"),
                new Product(++_instanceCounter, "Товар 7", 2750, "Описание 7"),
                new Product(++_instanceCounter, "Товар 8", 500, "Описание 8")
            ];
        }
        public List<Product> GetAll() => _products;

        public Product? TryGetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            product.Id = ++_instanceCounter;

            _products.Add(product);
        }

        [HttpPost]
        public void Update(Product product)
        {
            var existingProduct = TryGetById(product.Id);

            if (existingProduct is not null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Cost = product.Cost;
                existingProduct.Description = product.Description;
            }
        }

        public void Delete(int id)
        {
            var existingProduct = TryGetById(id);

            if (existingProduct != null)
            {
                _products.Remove(existingProduct);
            }
        }
    }
}
