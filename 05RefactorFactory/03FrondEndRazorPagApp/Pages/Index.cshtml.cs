using _03FrondEndRazorPagApp.Model;
using _03FrondEndRazorPagApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace _03FrondEndRazorPagApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;

        [BindProperty]
        public IEnumerable<Product> Products { get; set; }=new List<Product>();

        public IndexModel(ILogger<IndexModel> logger, IProductService productService)
        {
            _logger = logger;
           _productService = productService;
			logger.LogWarning(_productService.GetHashCode().ToString());
		}

        public async Task OnGet()
        {
            Products = await _productService.GetProducts();
        }
    }
}
