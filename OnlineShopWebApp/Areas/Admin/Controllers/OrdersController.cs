using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrdersController : Controller
    {
        private readonly IOrdersManager ordersManager;
        private readonly IMapper mapper;

        public OrdersController(IOrdersManager ordersManager, IMapper mapper)
        {
            this.ordersManager = ordersManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await ordersManager.GetAllAsync();
            return View(mapper.Map<List<OrderViewModel>>(orders));
        }

        public async Task<IActionResult> OrderDetails(Guid id)
        {
            var order = await ordersManager.TryGetByIdAsync(id);
            return View(mapper.Map<OrderViewModel>(order));
        }

        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatusViewModel status)
        {
            await ordersManager.UpdateStatusAsync(id, (OrderStatus)status);
            return RedirectToAction("Index");
        }

    }
}