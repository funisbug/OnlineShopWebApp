using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal Total
        {
            get
            {
                return Items.Sum(x => x.Cost);
            }
        }
        public int Amount
        {
            get
            {
                return Items.Sum(x => x.Amount);
            }
        }
    }    
}
