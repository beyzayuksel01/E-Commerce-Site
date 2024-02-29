using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.ViewModels;
using System.Diagnostics;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository; 
        private readonly ICategoryRepository _categoryRepository; 
        private readonly ICommentRepository _commentRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IBrandRepository brandRepository, UserManager<AppUser> userManager)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _brandRepository = brandRepository;
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            var products = _productRepository.GetAllProducts().ToList();
            var brands = _brandRepository.GetAllBrands().ToList();
            var categories = _categoryRepository.GetAllCategories().ToList();
            var comments = _commentRepository.GetAllComments().ToList();
            var users = _userManager.Users.ToList();




            var viewModel = new HomeIndexViewModel
            {
                Products = products,
                Brands = brands,
                Categories = categories,
                Comments = comments,
                Users = users
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
