using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IComparisonService
    {
        Task<ComparisonDto> GetByUserIdAsync(string userId);
        Task AddToComparison(int productId, string userId);
        Task RemoveFromComparison(int productId, string userId);
        Task ClearComparison(string userId);
    }
}
