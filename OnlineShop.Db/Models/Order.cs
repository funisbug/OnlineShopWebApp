using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
	{
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public OrderData OrderData { get; set; }
        public DateTime DateTime { get; set; }
		public List<CartItem> CartItems { get; set; }
        public OrderStatus Status { get; set; }
    }
}
