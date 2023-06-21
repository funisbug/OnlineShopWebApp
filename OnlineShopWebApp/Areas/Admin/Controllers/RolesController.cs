using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(mapper.Map<List<RoleViewModel>>(roles));
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel role)
        {            
            if (roleManager.FindByNameAsync(role.Name).Result != null)
            {
                ModelState.AddModelError("Name", "Такая роль уже существует");
            }
            if (ModelState.IsValid)
            {
                await roleManager.CreateAsync(new IdentityRole(role.Name));
                return RedirectToAction("Index");
            }
            return View(role);
        }

        public async Task<IActionResult> RemoveRole(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
