using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly ICartsManager cartsManager;
        private readonly IOrdersManager ordersManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderController(ICartsManager cartsManager, IOrdersManager ordersManager, IHttpContextAccessor httpContextAccessor)
        {
            this.cartsManager = cartsManager;
            this.ordersManager = ordersManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderData orderData)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var cart = await cartsManager.TryGetByUserIdAsync(user.Email);
            if (cart != null)
            {
                var cartItems = cart.Items;
                await ordersManager.AddAsync(orderData, cartItems, user.Email);
                await cartsManager.RemoveCartAsync(user.Email);
                return Ok(new { Message = "Order created" });
            }
            return BadRequest("Cart is empty");
        }
    }
}
