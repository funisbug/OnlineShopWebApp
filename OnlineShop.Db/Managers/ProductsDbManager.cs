using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Managers
{
    public class ProductsDbManager : IProductsManager
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbManager(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products.ToListAsync();
        }

        public async Task<Product> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            databaseContext.Products.Add(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await TryGetByIdAsync(product.Id);
            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;
            existingProduct.ImagePath = product.ImagePath;
            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await TryGetByIdAsync(id);
            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await databaseContext.Products.ToListAsync();
            }
            var searchProducts = await databaseContext.Products.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return searchProducts;
        }
    }
}
