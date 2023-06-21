using System.Collections.Generic;
using System;

namespace OnlineShop.Db.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Items { get; set; }
    }
}
