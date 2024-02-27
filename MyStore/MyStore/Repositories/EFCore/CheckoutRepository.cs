using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.ViewModels;

namespace MyStore.Repositories.EFCore
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckoutRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public void AddOrder(string userId, int productId)
        {

            var product = _context.Products.Find(productId);

            var checkoutViewModel = new CheckOutViewModel
            {
                Name = product.Name,
                BrandName = product.Brand != null ? product.Brand.BrandName : "Bilinmiyor",
                CategoryName = product.Category != null ? product.Category.CategoryName : "Bilinmiyor",
                Price = product.Price,
                Quantity = 1
            };

        }
    }
}
