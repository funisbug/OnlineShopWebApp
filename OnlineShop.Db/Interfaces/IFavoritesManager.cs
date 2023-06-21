using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoritesManager
    {
        Task<Favorite> TryGetByUserIdAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task RemoveAsync(Guid productId, string userId);
    }
}
