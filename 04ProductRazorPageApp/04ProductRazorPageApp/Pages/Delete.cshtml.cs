using _04ProductRazorPageApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace _04ProductRazorPageApp.Pages
{
    public class DeleteModel : PageModel
    {
		private readonly IHttpClientFactory _httpClientFactory;

		[BindProperty]
		public Product Product { get; set; } = new Product();

		public DeleteModel(IHttpClientFactory httpClientFactory, ILogger<DeleteModel> logger
			)
		{
			_httpClientFactory = httpClientFactory;
			logger.LogWarning(httpClientFactory.GetHashCode().ToString());
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
			using HttpResponseMessage response = await httpClient.DeleteAsync($"products/{Product.Id}");
			if (response.IsSuccessStatusCode)
			{
				TempData["success"] = "Data was deleted successfully.";
			}
			else TempData["failure"] = "Operation was not successful";
			return RedirectToPage("Index");
		}


	}
}
