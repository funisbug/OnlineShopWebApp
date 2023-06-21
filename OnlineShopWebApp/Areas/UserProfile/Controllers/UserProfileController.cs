using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.UserProfile.Controllers
{
    [Area("UserProfile")]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IOrdersManager ordersManager;
        private readonly ImageProvider imageProvider;
        private readonly IMapper mapper;

        public UserProfileController(UserManager<User> userManager, IOrdersManager ordersManager, ImageProvider imageProvider, IMapper mapper)
        {
            this.userManager = userManager;
            this.ordersManager = ordersManager;
            this.imageProvider = imageProvider;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            return View(mapper.Map<UserViewModel>(user));
        }

        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {                
                var currentUser = await userManager.GetUserAsync(User);
                if (user.UploadedFile != null)
                {
                    currentUser.ImagePath = imageProvider.SafeFile(user.UploadedFile, ImageFolders.Profiles);
                }
                currentUser.Name = user.Name;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Adress = user.Adress;
                await userManager.UpdateAsync(currentUser);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> ChangePassword()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var user = new Registration { Email = currentUser.Email };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(Registration user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.FindByEmailAsync(user.Email);
                var result = await userManager.ChangePasswordAsync(currentUser, user.CurrentPassword, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(user);            
        }

        public async Task<IActionResult> Orders()
        {
            var user = await userManager.GetUserAsync(User);
            var orders = await ordersManager.TryGetByEmailAsync(user.Email);
            return View(mapper.Map<List<OrderViewModel>>(orders));
        }
    }
}