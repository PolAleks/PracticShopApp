using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryCartsRepository : ICartsRepository
    {
        private readonly List<Cart> _carts = [];

        public Cart? TryGetByUserId(string userId)
        {
            return _carts.FirstOrDefault(c => c.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart is null)
            {
                _carts.Add(new Cart
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

        public void Subtract(int productId, string userId)
        {
            var existingCart = TryGetByUserId(userId);

            var existingCartItem = existingCart?.Items?.FirstOrDefault(item => item.Product?.Id == productId);

            if (existingCartItem is not null)
            {
                if (--existingCartItem.Quantity == 0)
                {
                    existingCart!.Items!.Remove(existingCartItem);
                }
            }
        }

        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart is not null)
            {
                _carts.Remove(existingCart);
            }
        }
    }
}
