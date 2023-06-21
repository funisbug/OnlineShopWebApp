using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Не указана роль")]
        public string Name { get; set; }
    }
}
