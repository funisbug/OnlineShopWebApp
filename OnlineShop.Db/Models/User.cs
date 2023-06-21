using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string ImagePath { get; set; }
    }
}
