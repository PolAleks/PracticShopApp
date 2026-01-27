using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public static class ProductsRepository
    {
        private static int _instanceCounter = 0;
        private static readonly List<Product> _products =
            [
                new Product(++_instanceCounter, "Товар 1", 1000, "Описание 1"),
                new Product(++_instanceCounter, "Товар 2", 2000, "Описание 2"),
                new Product(++_instanceCounter, "Товар 3", 1500, "Описание 3")
            ];

        public static List<Product> GetAll() => _products;
    }
}
