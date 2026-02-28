using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class ProductsDbRepository(DatabaseContext databaseContext) : IProductsRepository
    {
        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public void Delete(int productId)
        {
            var existingProduct = TryGetById(productId);
            
            if (existingProduct != null)
            {
                databaseContext.Products.Remove(existingProduct);
                databaseContext.SaveChanges();
            }
        }

        public List<Product> GetAll()
        {
            return databaseContext.Products.ToList();
        }

        public List<Product> Search(string query)
        {
            return databaseContext.Products.Where(product => product.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Product? TryGetById(int productId)
        {
             return databaseContext.Products.FirstOrDefault(product => product.Id.Equals(productId));
        }

        public void Update(Product product)
        {
            var updatedProduct = TryGetById(product.Id);
            if (updatedProduct != null)
            {
                updatedProduct.Name = product.Name;
                updatedProduct.Cost = product.Cost;
                updatedProduct.Description = product.Description;

                databaseContext.SaveChanges();
            }
        }
    }
}
