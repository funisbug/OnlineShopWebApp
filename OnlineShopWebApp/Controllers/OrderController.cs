using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsManager cartsManager;
        private readonly IOrdersManager ordersManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public OrderController(ICartsManager cartsManager, IOrdersManager ordersManager, UserManager<User> userManager, IMapper mapper)
		{
			this.cartsManager = cartsManager;
			this.ordersManager = ordersManager;
            this.userManager = userManager;
            this.mapper = mapper;
		}

		public IActionResult Index()
        {   
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Successful(OrderDataViewModel orderData)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var cart = await cartsManager.TryGetByUserIdAsync(user.Email);
                var cartItems = cart.Items;
                var orderDataDb = mapper.Map<OrderData>(orderData);
                await ordersManager.AddAsync(orderDataDb, cartItems, user.Email);
                await cartsManager.RemoveCartAsync(user.Email);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
