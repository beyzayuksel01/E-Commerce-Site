namespace MyStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? BrandId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
        public decimal TotalPrice => Quantity * Price; 
        public DateTime OrderDate { get; set; }

        public Product? Product { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public string? UserName { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
