using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.Repositories.EFCore;

namespace MyStore.Repositories.EFCore.Config
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IQueryable<Category> Categories => _context.Categories;


        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }


        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }


        public bool UpdateCategory(int categoryId, Category updatedCategory)
        {
            var existingCategory = _context.Categories.Find(categoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = updatedCategory.CategoryName;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }



        private void Save()
        {
            _context.SaveChanges();
        }
    }
}
