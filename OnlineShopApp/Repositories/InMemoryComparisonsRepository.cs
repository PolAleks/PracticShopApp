using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryComparisonsRepository : IComparisonRepository
    {
        private readonly List<Comparison> _comparisons = [];

        public void Add(Product product, string userId)
        {
            var comparison = TryGetByUserId(userId);

            if (comparison is null)
            {
                comparison = new Comparison
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<Product> { product }
                };
                _comparisons.Add(comparison);
            }
            else
            {
                if (!comparison.Items.Any(p => p.Id == product.Id))
                {
                    comparison.Items.Add(product);
                }
            }
        }

        public void Clear(string userId)
        {
            var comparison = TryGetByUserId(userId);
            if (comparison is not null)
            {
                _comparisons.Remove(comparison);
            }
        }

        public void Delete(int productId, string userId)
        {
            var comparison = TryGetByUserId(userId);

            var product = comparison?.Items.FirstOrDefault(p => p.Id == productId);

            if (product is not null)
            {
                comparison!.Items.Remove(product);
            }
        }

        public Comparison? TryGetByUserId(string userId)
        {
            return _comparisons.FirstOrDefault(c => c.UserId == userId);
        }
    }
}
