using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "You are required to enter a name.")]
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
