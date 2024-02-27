using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Repositories.Contract;

namespace MyStore.Repositories.EFCore
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Comment> Comments => _context.Comments;

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void CreateReply(Comment reply)
        {
            _context.Comments.Add(reply);
            _context.SaveChanges();
        }

        public  Comment GetCommentById(int commentId)
        {
            return _context.Comments.Find(commentId);
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
