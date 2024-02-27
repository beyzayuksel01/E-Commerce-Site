using MyStore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using MyStore.Models;
using Microsoft.EntityFrameworkCore;

namespace MyStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool result = _categoryRepository.UpdateCategory(id, category);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int id)
        {

            var categoryToDelete = await _categoryRepository.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(categoryToDelete);

            return RedirectToAction("Index");
        }

    }
}
