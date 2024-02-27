using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.ViewModels;
using System.Security.Claims;

namespace MyStore.Controllers
{
    public class CommentController : Controller
    {

        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _userManager = userManager;

        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(int productId, string text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                ProductId = productId,
                UserId = user.UserName, 
                Text = text,
                PublishedOn = DateTime.Now
            };

            _commentRepository.CreateComment(comment);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> List(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            string userName = user.UserName;

            var comments = await _commentRepository.Comments
                .Where(c => c.ProductId == productId && c.UserId == userName) 
                .OrderByDescending(c => c.PublishedOn)
                .ToListAsync();

            return View(comments);
        }

        public async Task<IActionResult> GetAllComments(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            string userName = user.UserName;

            var comments = await _commentRepository.Comments
                .Where(c => c.ProductId == productId && c.UserId == userName) 
                .Include(c => c.Product)
                .ThenInclude(p => p.Brand)
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();

            return View(comments);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int commentId)
        {
            Comment comment =  _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }

           _commentRepository.DeleteComment(commentId);
            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReply(int commentId, string textReply)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            //var CommentId = commentId;

            var comment = _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var reply = new Comment
            {
                ProductId = comment.ProductId,
                UserId = user.UserName,
                Text = textReply,
                PublishedOn = DateTime.Now
               
            };

            _commentRepository.CreateReply(reply);


            return RedirectToAction("Index", "Home");
        }
    }
}

