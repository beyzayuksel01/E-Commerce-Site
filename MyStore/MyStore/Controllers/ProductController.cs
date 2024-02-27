using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.Repositories.EFCore;
using MyStore.Repositories.EFCore.Config;
using MyStore.ViewModels;

namespace MyStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository productRepository , ICategoryRepository categoryRepository, IBrandRepository brandRepository, IOrderRepository orderRepository, ICommentRepository commentRepository , IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _orderRepository = orderRepository;
            _commentRepository = commentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index() //degisiklik yapıldı
        {

            var products = _productRepository.Products
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.Pictures);

            return View(await products.ToListAsync());



        }

        public IActionResult Details(int productId)
        {
            var product = _productRepository.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.ProductId == productId);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult ViewProductImages()
        {
            var images = GetProductImages();
            return View(images);
        }

        public List<string> GetProductImages()
        {
            var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            var imageFiles = Directory.GetFiles(imagesDirectory, "*.jfif").ToList();
            imageFiles.AddRange(Directory.GetFiles(imagesDirectory, "*.jpeg"));
            imageFiles.AddRange(Directory.GetFiles(imagesDirectory, "*.png"));
            return imageFiles;
        }


        public async Task< IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {          

                var prdct = new Product();

                prdct.Name = model.Name;
                prdct.Price = model.Price;
                prdct.Quantity = model.Quantity;

                if (model.CategoryId != null)
                {
                    prdct.CategoryId = model.CategoryId;
                }
                if (model.BrandId != null)
                {
                    prdct.BrandId = model.BrandId;
                }


            foreach (var pictureFile in model.Pictures)
            {
                var imageUrl = _productRepository.ProcessAndSaveImage(pictureFile);
                prdct.Pictures.Add(new Picture { Path = imageUrl });
            }


            _productRepository.CreateProduct(prdct);
                return RedirectToAction("Index");
  
        }


        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }



            ProductEditViewModel productVM = new ProductEditViewModel
            {
                Name = product.Name,
                ProductId = product.ProductId,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Quantity = product.Quantity,
                Tags = product.Tags
            };

            ViewBag.Brands = new SelectList(_brandRepository.GetAllBrands(), "BrandId", "BrandName");
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "CategoryName");

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel productVM)
        {


            Product product = _productRepository.Products
                                    .Include(p => p.Pictures)
                                    .FirstOrDefault(p => p.ProductId == productVM.ProductId);
            productVM.OldPictures = product.Pictures;


            if (ModelState.IsValid)
            {      
                bool result = _productRepository.UpdateProduct(productVM);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(productVM);
        }




        public IActionResult Delete(int id)
        {
            Product product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePOST(int id)
        {
            var productToDelete = await _productRepository.Products.FirstOrDefaultAsync(c => c.ProductId == id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(productToDelete);

            return RedirectToAction("Index");
        }


        public IActionResult ProductsByCategory(string categoryName)
        {
            var products = _productRepository.GetProductsByCategory(categoryName);
            return View(products);
        }
    }
}
