using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsManager
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> TryGetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
        Task<List<Product>> SearchAsync(string name);
    }
}
