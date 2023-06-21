using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebAPI.Models
{
    public class Authorization
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
