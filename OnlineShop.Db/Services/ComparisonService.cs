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
        public async Task AddToComparison(int productId, string userId)
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

        public async Task ClearComparison(string userId)
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

        public async Task RemoveFromComparison(int productId, string userId)
        {
            var comparison = await GetOrCreateComparisonAsync(userId);

            var existingProduct = comparison.Products?.FirstOrDefault(p => p.Id == productId);

            if (existingProduct != null)
            {
                comparison.Products!.Remove(existingProduct);
                await context.SaveChangesAsync();
            }
        }

        private async Task<Comparison> GetOrCreateComparisonAsync(string userId)
        {
            var comparison = await context.Comparisons
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if(comparison == null)
            {
                comparison = new() { UserId = userId};
                context.Comparisons.Add(comparison);
                await context.SaveChangesAsync();
            }

            return comparison;
        }
    }
}
