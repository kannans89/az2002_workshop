using _04ProductRazorPageApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace _04ProductRazorPageApp.Pages
{
    public class EditModel : PageModel
    {
		private readonly IHttpClientFactory _httpClientFactory;

		[BindProperty]
		public Product Product { get; set; } = new Product();

		public EditModel(IHttpClientFactory factory, ILogger<EditModel> logger)
		{

			_httpClientFactory = factory;
			logger.LogWarning(factory.GetHashCode().ToString());
		}
		public async Task OnGet(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("ProductApi");
			using HttpResponseMessage response = await httpClient.GetAsync($"products/{id}");
			if (response.IsSuccessStatusCode)
			{
				using var contentStream = await response.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				};
				Product = await JsonSerializer.DeserializeAsync<Product>(contentStream, options);
			}
		}
		public async Task<IActionResult> OnPost()
		{
			var httpClient = _httpClientFactory.CreateClient("ProductApi");
			var content = new StringContent(JsonSerializer.Serialize(Product), Encoding.UTF8, "application/json");
			using HttpResponseMessage response = await httpClient.PutAsync($"products/{Product.Id}", content);
			if (response.IsSuccessStatusCode)
			{
				TempData["success"] = "Data was updated successfully.";
			}
			else TempData["failure"] = "Operation was not successful";
			return RedirectToPage("Index");
		}
	}
}
