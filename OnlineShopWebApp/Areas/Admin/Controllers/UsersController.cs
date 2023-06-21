using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class Users : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public Users(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = userManager.Users;
            return View(mapper.Map<List<UserViewModel>>(users));
        }

        public async Task<IActionResult> UserDetails(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return View(mapper.Map<UserViewModel>(user));
        }

        public IActionResult ChangePassword(string email)
        {
            var user = new Registration { Email = email };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(Registration user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.FindByEmailAsync(user.Email);
                var newHashPassword = userManager.PasswordHasher.HashPassword(currentUser, user.Password);
                currentUser.PasswordHash = newHashPassword;
                await userManager.UpdateAsync(currentUser);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> EditUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.FindByIdAsync(user.Id);
                currentUser.Email = user.Email;
                currentUser.UserName = user.Email;
                currentUser.Name = user.Name;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Adress = user.Adress;
                await userManager.UpdateAsync(currentUser);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> EditRole(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var userRoles = await userManager.GetRolesAsync(user);
            var roles = roleManager.Roles.ToList();
            return View(new EditRoleViewModel
            {
                Email = user.Email,
                UserRoles = userRoles.Select(x => new RoleViewModel { Name = x }).ToList(),
                AllRoles = roles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string email, Dictionary<string, string> userRolesViewModel)
        {
            var userSelectedRoles = userRolesViewModel.Select(x => x.Key);
            var user = await userManager.FindByEmailAsync(email);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);
            await userManager.AddToRolesAsync(user, userSelectedRoles);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
