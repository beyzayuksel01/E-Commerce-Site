using System.Collections.Generic;
using MyStore.Models;
using MyStore.ViewModels;

namespace MyStore.Repositories.Contract
{
    
    public interface IProductRepository 
    {
        IEnumerable<Product> GetAllProducts();

        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Brand> Brands { get; }
        List<Product> GetProductsByCategory(string categoryName);

        void CreateProduct(Product product);
        Task<Product> GetProductByIdAsync(int productId);

        Product GetProductById(int productId);
        bool UpdateProduct(ProductEditViewModel product);
        void DeleteProduct(Product product);
        string ProcessAndSaveImage(IFormFile imageFile);
    }
    
}
