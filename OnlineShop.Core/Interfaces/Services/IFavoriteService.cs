using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IFavoriteService
    {
        Task<FavoriteDto> GetByUserIdAsync(string userId);
        Task AddToFavoriteAsync(int productId, string userId);
        Task RemoveFromFavoriteAsync(int productId, string userID);
        Task ClearFavoriteAsync(string userId);
        Task MergeFavoriteAsync(string sourceUserName, string destinationUserName);
    }
}
