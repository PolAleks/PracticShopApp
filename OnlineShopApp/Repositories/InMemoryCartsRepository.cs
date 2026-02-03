using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryCartsRepository : ICartsRepository
    {
        private readonly List<Cart> _card = [];

        public Cart? TryGetByUserId(string userId)
        {
            return _card.FirstOrDefault(c => c.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart is null)
            {
                _card.Add(new Cart
                {
                    Id = new Guid(),
                    UserId = userId,
                    Items = new List<CartItem>()
                    {
                        new CartItem()
                        {
                            Id = new Guid(),
                            Product = product,
                            Quantity = 1
                        }
                    }
                });
            }
            else
            {
                var existingItem = existingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
                if (existingItem is null)
                {
                    existingCart.Items.Add(new CartItem() { Id = new Guid(), Product = product, Quantity = 1 });
                }
                else
                {
                    existingItem.Quantity++;
                }
            }
        }
    }
}
