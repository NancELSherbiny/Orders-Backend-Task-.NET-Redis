using DAL.Models;

namespace DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<List<Order>> GetAllAsync();
        Task CreateAsync(Order order);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
