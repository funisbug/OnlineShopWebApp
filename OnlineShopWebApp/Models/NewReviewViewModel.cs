using System.ComponentModel.DataAnnotations;
using System;

namespace OnlineShopWebApp.Models
{
    public class NewReviewViewModel
    {
        public Guid ProductId { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public int Grade { get; set; }
    }
}
