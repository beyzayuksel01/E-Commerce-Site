using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Models;
using MyStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Repositories.Contract;
using Microsoft.AspNetCore.Hosting;
using MyStore.ViewModels;
using Microsoft.AspNetCore.Hosting.Server;

namespace MyStore.Repositories.EFCore.Config
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
        }

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<Brand> Brands => _context.Brands;

        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Pictures);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }



        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public bool UpdateProduct(ProductEditViewModel updatedProduct)
        {
            var existingProduct = _context.Products.Find(updatedProduct.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                existingProduct.CategoryId = updatedProduct.CategoryId;
                existingProduct.BrandId = updatedProduct.BrandId;
                existingProduct.IsActive = updatedProduct.IsActive;



                foreach (var picture in existingProduct.Pictures.ToList())
                {
                    if (_webHostEnvironment != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, picture.Path);
                        File.Delete(filePath);
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(_webHostEnvironment));
                    }

                    existingProduct.Pictures.Remove(picture);
                }


                foreach (var pictureFile in updatedProduct.NewPictures)
                {
                    var imageUrl = ProcessAndSaveImage(pictureFile);
                    existingProduct.Pictures.Add(new Picture { Path = imageUrl });

                }


                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteProduct(Product product)
        {
            //_context.Products.Remove(product);
            product.IsActive = false;
            _context.SaveChanges();
        }

        public List<Product> GetProductsByCategory(string categoryName)
        {
            return _context.Products
                .Where(p => p.Category.CategoryName == categoryName)
                .ToList();
        }

        private void Save()
        {
            _context.SaveChanges();
        }



        public string ProcessAndSaveImage(IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uniqueFileName = GetUniqueFileName2(imageFile.FileName);
                    string directoryPath;

                    if (_webHostEnvironment != null)
                    {
                        directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(_webHostEnvironment));
                    }


                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string filePath = Path.Combine(directoryPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    return $"img/{uniqueFileName}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing and saving image: {ex.Message}");
                throw;
            }

            return null;
        }

        private string GetUniqueFileName2(string fileName)
        {
            string guid = Path.GetRandomFileName();
            return $"{guid}_{fileName}";
        }
    }
}







































//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MyStore.Models;
//using MyStore.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MyStore.Repositories.Contract;



//namespace MyStore.Repositories.EFCore.Config
//{
//    public sealed class ProductRepository : IProductRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public ProductRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public Product AddProduct(Product product)
//        {
//            _context.Products.Add(product);
//            Save();
//            return product;
//        }

//        public void CreateProduct(Product product)
//        {
//            _context.Products.Add(product);
//            Save();
//        }

//        public bool DeleteProduct(int productId)
//        {
//            var product = _context.Products.Find(productId);
//            if (product != null)
//            {
//                _context.Products.Remove(product);
//                Save();
//                return true;
//            }
//            return false;
//        }

//        public IEnumerable<Product> GetAllProducts()
//        {
//            return _context.Products.ToList();
//        }

//        public async Task<Product> GetProductByIdAsync(int productId)
//        {
//            return await _context.Products.FindAsync(productId);
//        }

//        public void Save()
//        {
//            _context.SaveChanges();
//        }

//        public void UpdateProduct(int productId, Product product)
//        {
//            var existingProduct = _context.Products.Find(productId);
//            if (existingProduct != null)
//            {
//                existingProduct.Name = product.Name;
//                existingProduct.CategoryName = product.CategoryName;
//                existingProduct.Price = product.Price;
//                existingProduct.Quantity = product.Quantity;
//                Save();
//            }
//        }
//    }
//}
