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
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IBrandRepository brandRepository, UserManager<AppUser> userManager, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _brandRepository = brandRepository;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            var products = _productRepository.GetAllProducts().ToList();
            var brands = _brandRepository.GetAllBrands().ToList();
            var categories = _categoryRepository.GetAllCategories().ToList();
            var comments = _commentRepository.GetAllComments().ToList();
            var orders = _orderRepository.GetAllOrders().ToList();
            var users = _userManager.Users.ToList();




            var viewModel = new HomeIndexViewModel
            {
                Products = products,
                Brands = brands,
                Categories = categories,
                Comments = comments,
                Orders = orders,
                Users = users
            };

            return View(viewModel);
        }



        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}


    }
}
