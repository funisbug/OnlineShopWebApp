using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OnlineShopWebApp.Services
{
    public interface IReviewApiClient
    {
        Task<List<ReviewViewModel>> GetByProductIdAsync(Guid productId);
        Task AddAsync(NewReviewViewModel review);
    }
}
