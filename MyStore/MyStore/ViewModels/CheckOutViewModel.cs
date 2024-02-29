using MyStore.Models;

namespace MyStore.ViewModels
{
    public class CheckOutViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        public AppUser? AppUser { get; set; }
        public Product? Product { get; set; }
        public Brand? Brand { get; set; }
        public Category? Category { get; set; }


    }
}
