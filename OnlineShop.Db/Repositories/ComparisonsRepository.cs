using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Repositories
{
    public class ComparisonsRepository(DatabaseContext databaseContext) : IComparisonRepository
    {
        public void Add(Product product, string userId)
        {
            Comparison? existingComparison = TryGetByUserId(userId);

            if (existingComparison == null)
            {
                Comparison newComparison = new()
                {
                    UserId = userId,
                    Products = [product]
                };

                databaseContext.Comparisons.Add(newComparison);
            }
            else
            {
                var existingProductInComparison = existingComparison.Products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProductInComparison == null)
                {
                    existingComparison.Products.Add(product);
                }
            }
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            Comparison? existingComparison = TryGetByUserId(userId);

            if (existingComparison != null)
            {
                databaseContext.Comparisons.Remove(existingComparison);
                databaseContext.SaveChanges();
            }
        }

        public void Delete(int productId, string userId)
        {
            Comparison? existingComparison = TryGetByUserId(userId);

            var existingProductInComparison = existingComparison?.Products.FirstOrDefault(p => p.Id.Equals(productId));

            if (existingProductInComparison != null)
            {
                existingComparison!.Products.Remove(existingProductInComparison);
                databaseContext.SaveChanges();
            }
        }

        public Comparison? TryGetByUserId(string userId)
        {
            return databaseContext.Comparisons
                .Include(comparison => comparison.Products)
                .FirstOrDefault(comparison => comparison.UserId == userId);
        }
    }
}
