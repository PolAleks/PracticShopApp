using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CartsRepository(DatabaseContext databaseContext) : ICartsRepository
    {

        public Cart? TryGetByUserId(string userId)
        {
            return databaseContext.Carts
                .Include(cart => cart.Items)
                .ThenInclude(cartItem => cartItem.Product)
                .FirstOrDefault(cart => cart.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart is null)
            {
                Cart newCart = new()
                {
                    UserId = userId,
                    Items = []
                };

                newCart.Items = [ new Item()
                {
                    Product = product,
                    Quantity = 1
                } ];

                databaseContext.Carts.Add(newCart);
            }
            else
            {
                var existingItem = existingCart.Items?.FirstOrDefault(item => item.Product.Id == product.Id);

                if (existingItem is null)
                {
                    Item item = new()
                    {
                        Cart = existingCart,
                        Product = product,
                        Quantity = 1
                    };

                    databaseContext.Items.Add(item);
                }
                else
                {
                    existingItem.Quantity++;
                }
            }

            databaseContext.SaveChanges();
        }

        public void Subtract(int productId, string userId)
        {
            var existingCart = TryGetByUserId(userId);

            var existingCartItem = existingCart?.Items?.FirstOrDefault(item => item.Product?.Id == productId);

            if (existingCartItem is null)
                return;

            existingCartItem.Quantity--;

            if (existingCartItem.Quantity == 0)
                existingCart?.Items?.Remove(existingCartItem);

            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart is not null)
            {
                databaseContext.Carts.Remove(existingCart);
                databaseContext.SaveChanges();
            }
        }
    }
}
