using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Helpers;
using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public IActionResult GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            return View(orders);
        }
        //public IActionResult Index()
        //{
        //    var userId = HttpContext.Session.GetString("UserId");
 

        //    var cartItems = _orderRepository.GetCartItemsByUserId(userId);
        //    return View(cartItems);
        //}

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = HttpContext.Session.GetString("UserId");
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            _orderRepository.AddToCart(productId, quantity, userId);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int cartItemId)
        {
            _orderRepository.RemoveFromCart(cartItemId);
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var orders = _orderRepository.GetCartItemsByUserId(userId);

            var orderViewModels = orders.Select(order => new OrderCreateViewModel
            {
                ProductId = order.ProductId,
                BrandId = order.BrandId,
                CategoryId = order.CategoryId,
                Price = order.Price,
                Quantity = order.Quantity
            }).ToList();

            return View(orderViewModels.ToList());
        }
    }

}

