using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IComparisonManager
    {
        Task<Comparison> TryGetByUserIdAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task RemoveAsync(Guid productId, string userId);
    }
}
