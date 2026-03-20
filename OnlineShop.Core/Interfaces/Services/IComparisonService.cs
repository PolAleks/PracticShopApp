using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IComparisonService
    {
        Task<ComparisonDto> GetByUserIdAsync(string userId);
        Task AddToComparisonAsync(int productId, string userId);
        Task RemoveFromComparisonAsync(int productId, string userId);
        Task ClearComparisonAsync(string userId);
        Task MergeComparisonAsync(string sourceUserName, string destinationUserName);
    }
}
