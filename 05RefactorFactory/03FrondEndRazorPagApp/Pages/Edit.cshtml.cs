using _03FrondEndRazorPagApp.Model;
using _03FrondEndRazorPagApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace _03FrondEndRazorPagApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public EditModel(IProductService productservice,ILogger<EditModel> logger) { 
        
          _productService = productservice;
			logger.LogWarning(_productService.GetHashCode().ToString());
		}
        public async Task OnGet(int id)
        {
            Product = await _productService.GetProductById(id);
        }

        public async Task<IActionResult> OnPost()
        {
         
            var success = await _productService.EditProduct(Product);
            if (success)
            {
                TempData["success"] = "Data was updated successfully.";
            }
            else TempData["failure"] = "Operation was not successful";
            return RedirectToPage("Index");
        }   
    }
}
