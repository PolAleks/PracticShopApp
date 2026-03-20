using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Exceptions;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Services
{
    public class CartService(DatabaseContext context,
                             IMapper mapper) : ICartService
    {
        private readonly DatabaseContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CartDto> GetCartAsync(string userName)
        {
            var cart = await GetOrCreateCartAsync(userName);

            var cartDto = _mapper.Map<CartDto>(cart);

            return cartDto;
        }

        public async Task AddToCartAsync(string userName, int productId)
        {
            var cart = await GetOrCreateCartAsync(userName);

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Item item = new()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };

                _context.Items.Add(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task IncreaseQuantityAsync(string userName, int productId)
        {
            var existingItem = await _context.Items
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Cart!.UserId == userName && ci.ProductId == productId);

            if (existingItem == null)
            {
                throw new NotFoundException("Данный товар не найден в корзине");
            }

            existingItem.Quantity++;

            await _context.SaveChangesAsync();
        }

        public async Task DecreaseQuantityAsync(string userName, int productId)
        {
            var existingItem = await _context.Items
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Cart!.UserId == userName && ci.ProductId == productId);

            if (existingItem == null)
            {
                throw new NotFoundException("Данный товар не найден в корзине");
            }

            if (existingItem.Quantity == 1)
            {
                _context.Items.Remove(existingItem);
            }
            else
            {
                existingItem.Quantity--;
            }
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string userName)
        {
            var cart = await GetOrCreateCartAsync(userName);

            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Cart> GetOrCreateCartAsync(string userName)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userName);

            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userName,
                    Items = []
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task MergeCartAsync(string anonymousUser, string authenticatedUser)
        {
            bool hasData = await HasDataAsync(anonymousUser);

            if (!hasData) return;

            var anonymousCart = await GetOrCreateCartAsync(anonymousUser);

            var userCart = await GetOrCreateCartAsync(authenticatedUser);

            foreach (var item in anonymousCart.Items)
            {
                var userItem = userCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

                if (userItem != null)
                {
                    userItem.Quantity += item.Quantity;
                }
                else
                {
                    userCart.Items.Add(new Item()
                    {
                        CartId = userCart.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }
            }

            _context.Carts.Remove(anonymousCart);

            await _context.SaveChangesAsync();
        }

        private async Task<bool> HasDataAsync(string userName)
        {
            return await _context.Carts
                .Where(c => c.UserId == userName)
                .Select(c => c.Items.Any())
                .FirstOrDefaultAsync();
        }
    }
}
