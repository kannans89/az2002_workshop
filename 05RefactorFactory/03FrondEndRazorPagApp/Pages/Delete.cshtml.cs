using _03FrondEndRazorPagApp.Model;
using _03FrondEndRazorPagApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace _03FrondEndRazorPagApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public Product Product { get; set; } = new Product();   

        public DeleteModel(IProductService productService,ILogger<DeleteModel> logger
            )
        {
           _productService  = productService;
            logger.LogWarning(_productService.GetHashCode().ToString());
        }
        public async Task OnGet(int id)
        {
            Product = await _productService.GetProductById(id);

        }

        public async Task<IActionResult> OnPost()
        {
            var success = await _productService.Delete(Product);
            if (success)
            {
                TempData["success"] = "Data was deleted successfully.";
            }
            else TempData["failure"] = "Operation was not successful";
            return RedirectToPage("Index");
        }
    }
}
