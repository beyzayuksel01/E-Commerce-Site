using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Helpers;
using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.ViewModels;

namespace MyStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<AppUser> _userManager;

        public CheckoutController(ICheckoutRepository checkoutRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _checkoutRepository = checkoutRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CheckOutViewModel>>("CartItems");

            if (cartItems == null || cartItems.Count == 0)
            {
                return View("EmptyCart");
            }

            return View(cartItems);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string userId = user.Id;

            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CheckOutViewModel>>("CartItems") ?? new List<CheckOutViewModel>();
            cartItems.Add(new CheckOutViewModel
            {
                Name = product.Name,
                BrandName = product.Brand != null ? product.Brand.BrandName : "Unknown",
                CategoryName = product.Category != null ? product.Category.CategoryName : "Unknown",
                Price = product.Price,
                Quantity = product.Quantity
            });
            HttpContext.Session.SetObjectAsJson("CartItems", cartItems);

            return RedirectToAction("Index", "Checkout");
        }


        [HttpPost]
        public IActionResult PlaceOrder(string cartNumber)
        {
            return Json(new { success = true });
        }

    }
}
