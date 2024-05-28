using _03FrondEndRazorPagApp.Model;

namespace _03FrondEndRazorPagApp.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task<bool> Delete(Product product);
        Task<bool> EditProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
    }
}