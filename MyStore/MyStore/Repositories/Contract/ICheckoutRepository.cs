using MyStore.Models;
using MyStore.ViewModels;

namespace MyStore.Repositories.Contract
{
    public interface ICheckoutRepository
    {
        void AddOrder(string userId, int productId);
    }
}
