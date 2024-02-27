using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Repositories.Contract;

namespace MyStore.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IActionResult Index()
        {
            var brands = _brandRepository.GetAllBrands();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.CreateBrand(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        public IActionResult Edit(int id)
        {
            var brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(int id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isUpdated = _brandRepository.UpdateBrand(id, brand);
                if (isUpdated)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(brand);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }

            _brandRepository.DeleteBrand(brand);
            return RedirectToAction("Index");
        }
    }
}
