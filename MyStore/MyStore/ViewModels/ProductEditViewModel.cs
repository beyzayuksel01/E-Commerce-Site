using MyStore.Models;

namespace MyStore.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }


        public List<Tag> Tags { get; set; } = new List<Tag> { };
        public List<Picture> OldPictures { get; set; } = new List<Picture>();

        public List<IFormFile> NewPictures { get; set; } = new List<IFormFile>();


    }
}
