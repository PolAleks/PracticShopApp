using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string userId);
        Task<CartDto> AddToCartAsync(string userId, int productId);
        Task<CartDto> IncreaseQuantityAsync(string userId, Guid cartItemId);
        Task<CartDto> DecreaseQuantityAsync(string userId, Guid cartItemId);
        Task ClearCartAsync(string userId);
    }
}
