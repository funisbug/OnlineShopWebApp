using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersManager
    {
        Task<List<Order>> GetAllAsync();
        Task<Order> TryGetByIdAsync(Guid id);
        Task AddAsync(OrderData order, List<CartItem> cartItems, string userId);
        Task UpdateStatusAsync(Guid id, OrderStatus status);
        Task<List<Order>> TryGetByEmailAsync(string email);
    }
}