using MyStore.Models;

namespace MyStore.Repositories.Contract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);

        IQueryable<Category> Categories { get; }

        bool UpdateCategory(int categoryId, Category updatedCategory);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
    }

}
