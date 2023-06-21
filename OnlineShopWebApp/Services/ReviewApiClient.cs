using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Services
{
    public class ReviewApiClient : IReviewApiClient
    {
        private readonly HttpClient httpClient;

        public ReviewApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ReviewViewModel>> GetByProductIdAsync(Guid productId)
        {
            string url = $"https://localhost:7274/api/Review/GetByProductId?productId={productId}";

            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                List<ReviewViewModel> reviews = await response.Content.ReadFromJsonAsync<List<ReviewViewModel>>();
                return reviews;
            }
            else
            {
                throw new Exception("Failed");
            }
        }

        public async Task AddAsync(NewReviewViewModel review)
        {
            string url = $"https://localhost:7274/api/Review/AddReview";
            await httpClient.PostAsJsonAsync(url, review);
        }
    }
}
