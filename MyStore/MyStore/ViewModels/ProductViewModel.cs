using MyStore.Models;

namespace MyStore.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? BrandName { get; set; }
        public Brand? Brand { get; set; }
        public string? CategoryName { get; set; }
        public Category? Category { get; set; }
        public string? Image { get; set; }
    }
}
