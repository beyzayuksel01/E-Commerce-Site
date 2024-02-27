namespace MyStore.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
