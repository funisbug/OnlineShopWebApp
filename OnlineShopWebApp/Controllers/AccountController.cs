using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
	public class AccountController : Controller
	{
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

		public IActionResult Login(string returnUrl)
		{
            if (returnUrl != null)
            {
                return View(new Authorization() { ReturnUrl = returnUrl });
            }
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(Authorization authorization)
		{            
			if (ModelState.IsValid)
			{
                var result = await signInManager.PasswordSignInAsync(authorization.Email, authorization.Password, authorization.RememberMe, false);
                if (result.Succeeded)
                {
                    if (authorization.ReturnUrl != null)
                    {
                        return Redirect(authorization.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
			return View(authorization);
		}

        public IActionResult SignUp(string returnUrl)
        {
            if (returnUrl != null)
            {
                return View(new Registration() { ReturnUrl = returnUrl });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Registration registration)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = registration.Email, UserName = registration.Email };
                var result = await userManager.CreateAsync(user, registration.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.UserRoleName);
                    await signInManager.SignInAsync(user, false);
                    if (registration.ReturnUrl != null)
                    { 
                        return Redirect(registration.ReturnUrl);
                    }                        
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registration);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
