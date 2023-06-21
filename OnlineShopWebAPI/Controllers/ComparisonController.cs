using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ComparisonController : Controller
    {
        private readonly IComparisonManager comparisonManager;
        private readonly IProductsManager productsManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ComparisonController(IComparisonManager comparisonManager, IProductsManager productsManager, IHttpContextAccessor httpContextAccessor)
        {
            this.comparisonManager = comparisonManager;
            this.productsManager = productsManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetComparisonList")]
        public async Task<Comparison> GetComparisonList()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var comparisonList = await comparisonManager.TryGetByUserIdAsync(user.UserName);
            return comparisonList;
        }

        [HttpPost("AddToComparisonList")]
        public async Task<IActionResult> Add(Guid productId)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var product = await productsManager.TryGetByIdAsync(productId);            
            if (product != null)
            {
                await comparisonManager.AddAsync(product, user.UserName);
                return Ok(new { Message = "Product added to comparison list" });
            }
            return BadRequest("Product did not found");
        }

        [HttpDelete("RemoveFromComparisonList")]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            var product = await productsManager.TryGetByIdAsync(productId);
            if (product != null)
            {
                await comparisonManager.RemoveAsync(productId, user.UserName);
                return Ok(new { Message = "Product removed from comparison list" });
            }
            return BadRequest("Product did not found");
        }
    }
}
