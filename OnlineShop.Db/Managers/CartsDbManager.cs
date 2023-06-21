using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Managers
{
    public class CartsDbManager : ICartsManager
    {
        private readonly DatabaseContext databaseContext;

        public CartsDbManager(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Cart> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId,
                };
                newCart.Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Amount = 1,
                        Product = product
                    }
                };
                await databaseContext.Carts.AddAsync(newCart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingCartItem != null)
                {
                    existingCartItem.Amount++;
                }
                else
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Amount = 1,
                        Product = product
                    });
                }
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid productId, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            existingCartItem.Amount--;
            if (existingCartItem.Amount == 0)
            {
                databaseContext.CartItems.Remove(existingCartItem);
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            foreach (var item in existingCart.Items)
            {
                databaseContext.CartItems.Remove(item);
            }
            databaseContext.Carts.Remove(existingCart);
            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveCartAsync(string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            databaseContext.Carts.Remove(existingCart);
            await databaseContext.SaveChangesAsync();
        }
    }
}
