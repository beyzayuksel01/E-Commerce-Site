using MyStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MyStore.ViewModels
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }    

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Image { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
