using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Exceptions;

namespace OnlineShop.Infrastructure.Services
{
    public class CartService(DatabaseContext context,
                             IMapper mapper) : ICartService
    {
        private readonly DatabaseContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var cartDto = _mapper.Map<CartDto>(cart);

            return cartDto;
        }

        public async Task<CartDto> AddToCartAsync(string userId, int productId)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItem item = new()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1                    
                };

                _context.CartItems.Add(item);
            }
            await _context.SaveChangesAsync();

            return await GetCartAsync(userId);
        }

        public async Task<CartDto> IncreaseQuantityAsync(string userId, Guid cartItemId)
        {
            var existingItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Cart!.UserId == userId && ci.Id == cartItemId);

            if (existingItem == null)
            {
                throw new NotFoundException("Данный товар не найден в корзине");
            }

            existingItem.Quantity++;

            await _context.SaveChangesAsync();

            return await GetCartAsync(userId);
        }

        public async Task<CartDto> DecreaseQuantityAsync(string userId, Guid cartItemId)
        {
            var exisringItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Cart.UserId == userId && ci.Id == cartItemId);

            if (exisringItem == null)
            {
                throw new NotFoundException("Данный товар не найден в корзине");
            }

            if (exisringItem.Quantity == 1)
            {
                _context.CartItems.Remove(exisringItem);
            }
            else
            {
                exisringItem.Quantity--;
            }
            await _context.SaveChangesAsync();

            return await GetCartAsync(userId);
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    Items = []
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
