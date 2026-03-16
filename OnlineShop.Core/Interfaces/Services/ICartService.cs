using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string userId);
        Task AddToCartAsync(string userId, int productId);
        Task IncreaseQuantityAsync(string userId, int productId);
        Task DecreaseQuantityAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}
