using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Не указан логин")]
        [RegularExpression(@"[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите верный E-mail")]        
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public RoleViewModel Role { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
