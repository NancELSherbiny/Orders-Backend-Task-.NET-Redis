using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
            => await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

        public async Task<List<Order>> GetAllAsync()
            => await _context.Orders.ToListAsync();

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
                _context.Orders.Remove(order);
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
