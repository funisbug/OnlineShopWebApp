using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class EditRoleViewModel
    {        
        public string Email { get; set; }
        public List<RoleViewModel> UserRoles { get; set; }
        public List<RoleViewModel> AllRoles { get; set; }
    }
}
