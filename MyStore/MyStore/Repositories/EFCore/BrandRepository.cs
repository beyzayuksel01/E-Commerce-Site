using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Repositories.Contract;

namespace MyStore.Repositories.EFCore
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Brand> Brands => _context.Brands;

        public IEnumerable<Brand> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public Brand GetBrandById(int id)
        {
            return _context.Brands.Find(id);
        }

        public void CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public bool UpdateBrand(int brandId, Brand updatedBrand)
        {
            var existingBrand = _context.Brands.Find(brandId);
            if (existingBrand != null)
            {
                existingBrand.BrandName = updatedBrand.BrandName;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteBrand(Brand brand)
        {
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }
    }
}
