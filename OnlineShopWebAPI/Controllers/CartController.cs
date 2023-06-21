using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
        private readonly ICartsManager cartsManager;
        private readonly IProductsManager productsManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartController(ICartsManager cartsManager, IProductsManager productsManager, IHttpContextAccessor httpContextAccessor)
        {
            this.productsManager = productsManager;
            this.cartsManager = cartsManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetCart")]
        public async Task<Cart> GetCart()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var cart = await cartsManager.TryGetByUserIdAsync(user.UserName);
            return cart;
        }

        [HttpPost("AddProductInCart")]
        public async Task<IActionResult> AddProduct(Guid productId)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];            
            var product = await productsManager.TryGetByIdAsync(productId);  
            if (product != null)
            {
                await cartsManager.AddAsync(product, user.UserName);
                return Ok(new { Message = "Added" });
            }
            return BadRequest("Product did not found");
        }

        [HttpDelete("RemoveProductFromCart")]
        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var product = await productsManager.TryGetByIdAsync(productId);
            if (product != null)
            {
                await cartsManager.RemoveAsync(productId, user.UserName);
                return Ok(new { Message = "Removed" });
            }
            return BadRequest("Product did not found");
        }

        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            await cartsManager.ClearAsync(user.UserName);
            return RedirectToAction("Index");
        }
    }
}
