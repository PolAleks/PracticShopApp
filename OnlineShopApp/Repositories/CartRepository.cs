using Microsoft.Extensions.Diagnostics.HealthChecks;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public static class CartRepository
    {
        private static readonly List<Cart> _card = [];

        public static Cart? TryGetbyUserId(string userId)
        {
            return _card.FirstOrDefault(c => c.UserId == userId);
        }
    }
}
