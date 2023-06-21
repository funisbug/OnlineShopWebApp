using System.Collections.Generic;
using System;
using System.Linq;
using OnlineShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using System.Threading.Tasks;

namespace OnlineShop.Db.Managers
{
    public class OrdersDbManager : IOrdersManager
    {
        private readonly DatabaseContext databaseContext;

        public OrdersDbManager(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await databaseContext.Orders.Include(x => x.CartItems).ThenInclude(x => x.Product).Include(x => x.OrderData).ToListAsync();
        }

        public async Task<Order> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Orders.Include(x => x.CartItems).ThenInclude(x => x.Product).Include(x => x.OrderData).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> TryGetByEmailAsync(string email)
        {
            var userOrders = await GetAllAsync();
            return userOrders.Where(x => x.UserId == email).ToList();
        }

        public async Task AddAsync(OrderData orderData, List<CartItem> cartItems, string userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderData = orderData,
                CartItems = cartItems,
                DateTime = DateTime.Now,
                Status = OrderStatus.Created
            };
            databaseContext.Orders.Add(order);
            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid id, OrderStatus status)
        {
            var order = await TryGetByIdAsync(id);
            order.Status = status;
            await databaseContext.SaveChangesAsync();
        }

    }
}

