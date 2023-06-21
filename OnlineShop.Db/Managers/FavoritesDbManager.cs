using System.Collections.Generic;
using System;
using System.Linq;
using OnlineShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using System.Threading.Tasks;

namespace OnlineShop.Db.Managers
{
    public class FavoritesDbManager : IFavoritesManager
    {
        private readonly DatabaseContext databaseContext;

        public FavoritesDbManager(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Favorite> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Favorites.Include(x => x.Items).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingFavoritesList = await TryGetByUserIdAsync(userId);
            if (existingFavoritesList == null)
            {
                var newFavoretesList = new Favorite
                {
                    UserId = userId,
                    Items = new List<Product> { product }
                };
                databaseContext.Favorites.Add(newFavoretesList);
            }
            else
            {
                var existingFavoritesListItem = existingFavoritesList.Items.FirstOrDefault(x => x.Id == product.Id);
                if (existingFavoritesListItem == null)
                {
                    existingFavoritesList.Items.Add(product);
                }
                if (existingFavoritesList.Items.Count > 5)
                {
                    existingFavoritesList.Items.RemoveAt(0);
                }
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid productId, string userId)
        {
            var existingFavoritesList = await TryGetByUserIdAsync(userId);
            var existingFavoritesListItem = existingFavoritesList.Items.FirstOrDefault(x => x.Id == productId);
            existingFavoritesList.Items.Remove(existingFavoritesListItem);
            await databaseContext.SaveChangesAsync();
        }
    }
}