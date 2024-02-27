using Microsoft.AspNetCore.Identity;

namespace MyStore.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

        public string? Image { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
