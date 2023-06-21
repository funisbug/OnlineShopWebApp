using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoritesManager favoritesManager;
        private readonly IProductsManager productsManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FavoriteController(IFavoritesManager favoritesManager, IProductsManager productsManager, IHttpContextAccessor httpContextAccessor)
        {
            this.favoritesManager = favoritesManager;
            this.productsManager = productsManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetFavoriteList")]
        public async Task<Favorite> GetFavoriteList()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var favoriteList = await favoritesManager.TryGetByUserIdAsync(user.UserName);
            return favoriteList;
        }

        [HttpPost("AddToFavoriteList")]
        public async Task<IActionResult> AddToFavoriteList(Guid productId)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var product = await productsManager.TryGetByIdAsync(productId);
            if (product != null)
            {
                await favoritesManager.AddAsync(product, user.UserName);
                return Ok(new { Message = "Product added to favorite list" });
            }
            return BadRequest("Product did not found");
        }

        [HttpDelete("RemoveFromFavoriteList")]
        public async Task<IActionResult> RemoveFromFavoriteList(Guid productId)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var product = await productsManager.TryGetByIdAsync(productId);
            if (product != null)
            {
                await favoritesManager.RemoveAsync(productId, user.UserName);
                return Ok(new { Message = "Product removed from favorite list" });
            }
            return BadRequest("Product did not found");
        }
    }
}
