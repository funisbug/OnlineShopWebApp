using System;

namespace OnlineShop.Db.Models
{
    public class OrderData
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }                
        public string Email { get; set; }               
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
    }
}
