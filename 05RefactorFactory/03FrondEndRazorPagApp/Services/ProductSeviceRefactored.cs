


using _03FrondEndRazorPagApp.Model;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace _03FrondEndRazorPagApp.Services
    {
        public class ProductServiceRefacoctored: IProductService
        {
            private readonly HttpClient _httpClient;
            private readonly JsonSerializerOptions _options;
            private readonly ILogger<ProductServiceRefacoctored> _logger;

            public ProductServiceRefacoctored(IHttpClientFactory httpClientFactory,ILogger<ProductServiceRefacoctored> logger)
            {
                _httpClient = httpClientFactory.CreateClient("ProductsApi");


            logger.LogWarning(_httpClient.GetHashCode().ToString());
                _options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

            _logger = logger;
            }

            private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(contentStream, _options);
            }

            public async Task<IEnumerable<Product>> GetProducts()
            {
            _logger.LogWarning(_httpClient.GetHashCode().ToString());
            var response = await _httpClient.GetAsync("products");
                return response.IsSuccessStatusCode ? await DeserializeResponse<IEnumerable<Product>>(response) : null;
            }

            public async Task<Product> AddProduct(Product product)
            {
            _logger.LogWarning(_httpClient.GetHashCode().ToString());
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("products", content);
                return response.IsSuccessStatusCode ? await DeserializeResponse<Product>(response) : null;
            }

            public async Task<Product> GetProductById(int id)
            {
            _logger.LogWarning(_httpClient.GetHashCode().ToString());
            var response = await _httpClient.GetAsync($"products/{id}");
                return response.IsSuccessStatusCode ? await DeserializeResponse<Product>(response) : null;
            }

            public async Task<bool> EditProduct(Product product)
            {
            _logger.LogWarning(_httpClient.GetHashCode().ToString());
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"products/{product.Id}", content);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> Delete(Product product)
            {
            _logger.LogWarning(_httpClient.GetHashCode().ToString());
            var response = await _httpClient.DeleteAsync($"products/{product.Id}");
                return response.IsSuccessStatusCode;
            }
        }
    }

