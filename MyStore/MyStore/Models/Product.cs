using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace MyStore.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public bool InCart { get; set; }
        public bool IsActive { get; set; } = true; //isactive 

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Tag> Tags { get; set; } = new List<Tag> { };

        public List<Picture> Pictures { get; set; } = new List<Picture>();

    }

    public class Picture
    {
        public int PictureId { get; set; }
        public string? Path { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [NotMapped]
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; } = null!;
    }
}
