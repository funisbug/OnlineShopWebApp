﻿using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }                
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Comparison> Comparisons { get; set; }
        public List<Favorite> Favorites { get; set;}
        public string ImagePath { get; set; }
    }
}
