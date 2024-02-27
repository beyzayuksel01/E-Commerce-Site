using MyStore.Models;

namespace MyStore.Repositories.Contract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }

        void CreateComment(Comment comment);
        IEnumerable<Comment> GetAllComments();

        Comment GetCommentById(int commentId);
        Task DeleteComment(int commentId);

        void CreateReply(Comment reply);
    }
}
