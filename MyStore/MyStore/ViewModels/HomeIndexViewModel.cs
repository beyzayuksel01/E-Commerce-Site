using MyStore.Models;

namespace MyStore.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Brand> Brands { get; set; } = new List<Brand>();
        public int ProductIdToAddToCart { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Order> Orders { get;  set; } = new List<Order>();
        public List<AppUser> Users { get; set; } = new List<AppUser>();
        public List<Picture> Pictures { get; set; } = new List<Picture>();



        public string? Id { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Image { get; set; } 
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public Product? Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? BrandName { get; set; }
        public Brand? Brand { get; set; }
        public string? CategoryName { get; set; }
        public Category? Category { get; set; }
        
    }
}
