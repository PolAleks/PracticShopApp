using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string userName);
        Task AddToCartAsync(string userName, int productId);
        Task IncreaseQuantityAsync(string userName, int productId);
        Task DecreaseQuantityAsync(string userName, int productId);
        Task ClearCartAsync(string userName);
        Task MergeCartAsync(string sourcUserName, string destinationUserName);
    }
}
