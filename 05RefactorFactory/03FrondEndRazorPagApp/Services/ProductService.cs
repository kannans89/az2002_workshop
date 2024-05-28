using _03FrondEndRazorPagApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace _03FrondEndRazorPagApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = new List<Product>();
            var httpClient = _httpClientFactory.CreateClient("ProductsApi");
            using HttpResponseMessage response = await httpClient.GetAsync("products");
            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(contentStream, options);
            }

            return products;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var responseProduct = new Product();
            var httpClient = _httpClientFactory.CreateClient("ProductsApi");
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync("products", content);

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                responseProduct = await JsonSerializer.DeserializeAsync<Product>(contentStream, options);
            }

            return responseProduct;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = new Product();
            var httpClient = _httpClientFactory.CreateClient("ProductsApi");
            using HttpResponseMessage response = await httpClient.GetAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                product = await JsonSerializer.DeserializeAsync<Product>(contentStream, options);
            }

            return product;
        }
        public async Task<bool> EditProduct(Product product)
        {
            var httpClient = _httpClientFactory.CreateClient("ProductsApi");
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync($"products/{product.Id}", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Product product)
        {
            var httpClient = _httpClientFactory.CreateClient("ProductsApi");
            using HttpResponseMessage response = await httpClient.DeleteAsync($"products/{product.Id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
