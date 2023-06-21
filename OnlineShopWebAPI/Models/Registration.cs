using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebAPI.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Не указан логин")]
        [RegularExpression(@"[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите верный E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не подтвержден пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RePassword { get; set; }
    }
}
