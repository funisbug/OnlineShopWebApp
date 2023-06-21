using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class OrderDataViewModel
    {  
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан E-mail")]
        [RegularExpression(@"[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите верный E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан адрес")]
        public string Address { get; set; }

        public string Comment { get; set; }
    }
}
