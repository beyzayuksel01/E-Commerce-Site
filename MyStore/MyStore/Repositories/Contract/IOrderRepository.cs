using MyStore.Models;

namespace MyStore.Repositories.Contract
{
    public interface IOrderRepository
    {
         List<Order> GetCartItemsByUserId(string userId);
        void AddToCart(int productId, int quantity, string userId);
        void RemoveFromCart(int cartItemId);
        IEnumerable<Order> GetAllOrders();
    }
}
