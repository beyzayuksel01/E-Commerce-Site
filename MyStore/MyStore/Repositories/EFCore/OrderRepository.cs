using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Repositories.Contract;
using MyStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Repositories.EFCore
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }
        public List<Order> GetCartItemsByUserId(string userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void AddToCart(int productId, int quantity, string userId)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.ProductId == productId && o.UserId == userId);

            if (existingOrder != null)
            {
                existingOrder.Quantity += quantity;
            }
            else
            {
                var order = new Order
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserId = userId
                };
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(int cartItemId)
        {
            var order = _context.Orders.Find(cartItemId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
