using _04ProductRazorPageApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace _04ProductRazorPageApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public string Message { get; set; }
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IndexModel(ILogger<IndexModel> logger,IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;

            logger.LogInformation("IndexModel constructor called");
            logger.LogWarning("factory hashcode is "+clientFactory.GetHashCode());
           
        }

        public async Task OnGet()
        {
            Message = "Hello from IndexModel ,onGET called";


            HttpClient httpClient = _clientFactory.CreateClient("ProductApi");
            Console.WriteLine("httpClient hashcode is " + httpClient.GetHashCode());
            using HttpResponseMessage response = await httpClient.GetAsync("products");
            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(contentStream, options);
            }

        }
    }
}
