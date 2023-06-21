using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OnlineShopWebApp.Models
{
    public class ProductViewModel
    {        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        [Range(0, int.MaxValue, ErrorMessage = "Не корректно указана цена")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Не указано описание")]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadedFile { get; set; }

        public List<ReviewViewModel> Reviews { get; set; }

        public NewReviewViewModel NewReview { get; set; }
    }
}
