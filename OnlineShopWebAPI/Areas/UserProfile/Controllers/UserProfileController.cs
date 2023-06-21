using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebAPI.Areas.UserProfile.Controllers
{
    [ApiController]
    [Area("UserProfile")]
    [Route("api/[area]/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOrdersManager ordersManager;

        public UserProfileController(IHttpContextAccessor httpContextAccessor, IOrdersManager ordersManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.ordersManager = ordersManager;
        }

        [HttpGet("GetUserInfo")]
        public User GetUserInfo()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            return user;
        }

        [HttpGet("GetOrders")]
        public async Task<List<Order>> GetOrders()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var orders = await ordersManager.TryGetByEmailAsync(user.Email);
            return orders;
        }
    }
}