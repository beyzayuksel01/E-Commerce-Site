namespace MyStore.Repositories.Contract
{
    public interface IRepositoryManager
    {
        IProductRepository Product {  get; }
        ICategoryRepository Category { get; }

    }
}
