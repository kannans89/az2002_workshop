using _03FrondEndRazorPagApp.Model;
using _03FrondEndRazorPagApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace _03FrondEndRazorPagApp.Pages
{
    public class AddModel : PageModel
    {
        private readonly IProductService _productService;


        [BindProperty]
        public Product Product { get; set; } = new Product();
        public AddModel(IProductService productService,ILogger<AddModel> logger)
        {
           _productService = productService;
			logger.LogWarning(productService.GetHashCode().ToString());
		}
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
           
            var product = await _productService.AddProduct(Product);
            if (product.Name.Length>0)
            {
                TempData["success"] = "Data was added successfully.";
              
            }
           else TempData["failure"] = "Operation was not successful";
            return RedirectToPage("Index");
        }
    }
}
