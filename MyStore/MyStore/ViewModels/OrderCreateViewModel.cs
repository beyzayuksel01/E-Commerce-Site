using MyStore.Models;

namespace MyStore.ViewModels
{
    public class OrderCreateViewModel
    {   
        public string? UserName { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Name { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? BrandId { get; set; }
        public string? BrandName { get; set; }
        public Brand? Brand { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
