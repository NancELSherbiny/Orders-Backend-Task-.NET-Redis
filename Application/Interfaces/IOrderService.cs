using DAL.Models;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderAsync(Guid id);
        Task<List<Order>> GetAllAsync();
        Task<Order> CreateAsync(Order order);
        Task<bool> DeleteAsync(Guid id);
    }
}
