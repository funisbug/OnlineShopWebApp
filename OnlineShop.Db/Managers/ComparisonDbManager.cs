using OnlineShop.Db.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using System.Threading.Tasks;

namespace OnlineShop.Db.Managers
{
    public class ComparisonDbManager : IComparisonManager
    {
        private readonly DatabaseContext databaseContext;

        public ComparisonDbManager(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Comparison> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Comparisons.Include(x => x.Items).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingComparerList = await TryGetByUserIdAsync(userId);
            if (existingComparerList == null)
            {
                var newComparerList = new Comparison
                {
                    UserId = userId,
                    Items = new List<Product> { product }
                };
                databaseContext.Comparisons.Add(newComparerList);
            }
            else
            {
                var existingComparerListItem = existingComparerList.Items.FirstOrDefault(x => x.Id == product.Id);
                if (existingComparerListItem == null)
                {
                    existingComparerList.Items.Add(product);
                }
                if (existingComparerList.Items.Count > 5)
                {
                    existingComparerList.Items.RemoveAt(0);
                }
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid productId, string userId)
        {
            var existingComparerList = await TryGetByUserIdAsync(userId);
            var existingComparerListItem = existingComparerList.Items.FirstOrDefault(x => x.Id == productId);
            existingComparerList.Items.Remove(existingComparerListItem);
            await databaseContext.SaveChangesAsync();
        }
    }
}
