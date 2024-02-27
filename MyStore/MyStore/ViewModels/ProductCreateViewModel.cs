using MyStore.Models;

namespace MyStore.ViewModels
{
    public class ProductCreateViewModel
    {
        public string? Name { get; set; }
        public int? BrandId { get; set; }
        public string? BrandName { get; set; }   
        public Brand? Brand { get; set; }
        public string? CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }
}
