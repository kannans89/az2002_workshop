using _04ProductRazorPageApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace _04ProductRazorPageApp.Pages
{
    public class AddModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public AddModel(IHttpClientFactory httpClientFactory, ILogger<AddModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            logger.LogWarning(httpClientFactory.GetHashCode().ToString());
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("ProductApi");
            var content = new StringContent(JsonSerializer.Serialize(Product), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync("products", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was added successfully.";

            }
            else TempData["failure"] = "Operation was not successful";
            return RedirectToPage("Index");
        }
    }
}
