using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Services
{
    public class ComparisonService(DatabaseContext context, IMapper mapper) : IComparisonService
    {
        public async Task AddToComparisonAsync(int productId, string userId)
        {
            var comparison = await GetOrCreateComparisonAsync(userId);

            var existProduct = comparison.Products.Any(p => p.Id == productId);

            if (!existProduct)
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (product != null)
                {
                    comparison.Products.Add(product);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task ClearComparisonAsync(string userId)
        {
            var comparison = await GetOrCreateComparisonAsync(userId);

            if (comparison != null)
            {
                context.Comparisons.Remove(comparison);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ComparisonDto> GetByUserIdAsync(string userId)
        {
            var comparison = await GetOrCreateComparisonAsync(userId);

            var comparisonDto = mapper.Map<ComparisonDto>(comparison);

            return comparisonDto;
        }

        public async Task RemoveFromComparisonAsync(int productId, string userId)
        {
            var comparison = await GetOrCreateComparisonAsync(userId);

            var existingProduct = comparison.Products?.FirstOrDefault(p => p.Id == productId);

            if (existingProduct != null)
            {
                comparison.Products!.Remove(existingProduct);
                await context.SaveChangesAsync();
            }
        }

        public async Task MergeComparisonAsync(string sourceUserName, string destinationUserName)
        {
            var hasData = await HasDataAsync(sourceUserName);

            if (!hasData) return;

            var anonymousComparison = await GetOrCreateComparisonAsync(sourceUserName);

            var userComparison = await GetOrCreateComparisonAsync(destinationUserName);

            foreach (var product in anonymousComparison.Products)
            {
                var userProduct = userComparison.Products.FirstOrDefault(p => p.Id == product.Id);
                if (userProduct == null)
                {
                    userComparison.Products.Add(product);
                }
            }
            context.Comparisons.Remove(anonymousComparison);

            await context.SaveChangesAsync();
        }

        private async Task<Comparison> GetOrCreateComparisonAsync(string userId)
        {
            var comparison = await context.Comparisons
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (comparison == null)
            {
                comparison = new() { UserId = userId };
                context.Comparisons.Add(comparison);
                await context.SaveChangesAsync();
            }

            return comparison;
        }

        private async Task<bool> HasDataAsync(string userName)
        {
            return await context.Comparisons
                .Where(c => c.UserId == userName)
                .Select(c => c.Products.Any())
                .FirstOrDefaultAsync();
        }
    }
}
