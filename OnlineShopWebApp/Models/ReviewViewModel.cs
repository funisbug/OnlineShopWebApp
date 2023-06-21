using System;

namespace OnlineShopWebApp.Models
{
    public class ReviewViewModel
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public int Grade { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
