using MyStore.Models;

namespace MyStore.Repositories.Contract
{
    public interface IBrandRepository
    {
        IQueryable<Brand> Brands { get; }

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int id);

        void CreateBrand(Brand brand);

        bool UpdateBrand(int brandId, Brand updatedBrand);

        void DeleteBrand(Brand brand);
    }
}
