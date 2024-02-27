using MyStore.Models;

namespace MyStore.Repositories.Contract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }
        void CreateTag(Tag tag);
    }
}
