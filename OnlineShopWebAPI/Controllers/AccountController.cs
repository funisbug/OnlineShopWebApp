using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Db.Models;
using OnlineShopWebAPI;
using OnlineShopWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShopWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class AccountController : ControllerBase
	{
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
                
		[HttpPost("Authorization")]
		public async Task<IActionResult> Authorization([FromBody] Authorization authUser)
		{
            var result = await signInManager.PasswordSignInAsync(authUser.Email, authUser.Password, false, false);
            if (result.Succeeded)
            {
                var tokenString = GenerateJwtToken(authUser.Email);
                return Ok(new { Token = tokenString, Message = "Success" });
            }            
            return BadRequest("Please pass the valid Username and Password");
		}
                
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] Registration regUser)
        {
            var user = new User { UserName = regUser.Email, Email = regUser.Email };
            var result = await userManager.CreateAsync(user, regUser.Password);            
            if (result.Succeeded)
            {
                var tokenString = GenerateJwtToken(user.Email);
                return Ok(new { Token = tokenString, Message = "Success" });
            }
            return BadRequest("Please pass the valid Username and Password");
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetResault")]
        public IActionResult GetResault()
        {
            return Ok("API Validated");
        }

        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}