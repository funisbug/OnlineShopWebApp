using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsManager
    {
        Task<Cart> TryGetByUserIdAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task RemoveAsync(Guid productId, string userId);
        Task ClearAsync(string userId);
        Task RemoveCartAsync(string userId);
    }
}
