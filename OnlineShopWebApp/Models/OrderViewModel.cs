using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
	{
        public Guid Id { get; set; }

        public string Login { get; set; }

        public OrderDataViewModel OrderData { get; set; }

        public DateTime DateTime { get; set; }        

		public List<CartItemViewModel> CartItems { get; set; }

        public OrderStatusViewModel Status { get; set; }

        public decimal Total
        {
            get
            {
                return CartItems.Sum(x => x.Cost);
            }
        }
    }
}
