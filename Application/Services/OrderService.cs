using Application.Interfaces;
using Application.Settings;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IRedisCacheService _cache;
        private readonly ILogger<OrderService> _logger;
        private readonly TimeSpan _ttl;

        public OrderService(
            IOrderRepository repo,
            IRedisCacheService cache,
            ILogger<OrderService> logger,
            IOptions<CacheSettings> cacheOptions)
        {
            _repo = repo;
            _cache = cache;
            _logger = logger;

            // Convert seconds ? TimeSpan
            _ttl = TimeSpan.FromSeconds(cacheOptions.Value.OrderTTLSeconds);
        }

        public async Task<Order?> GetOrderAsync(Guid id)
        {
            string cacheKey = $"order:{id}";
            _logger.LogInformation("Checking cache for order {OrderId}", id);

            // 1. Check cache
            var cached = await _cache.GetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                _logger.LogInformation("Cache hit for order {OrderId}", id);
                return JsonSerializer.Deserialize<Order>(cached);
            }

            _logger.LogInformation("Cache miss for order {OrderId}, fetching from DB", id);

            // 2. Fetch from DB
            var order = await _repo.GetByIdAsync(id);
            if (order != null)
            {
                var serialized = JsonSerializer.Serialize(order);
                _logger.LogInformation("Caching order {OrderId}", id);
                await _cache.SetAsync(cacheKey, serialized, _ttl);

            }

            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all orders from DB");
            return await _repo.GetAllAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.OrderId = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;

            _logger.LogInformation("Creating order {OrderId} for Customername {Customername}", order.OrderId, order.CustomerName);
            await _repo.CreateAsync(order);
            await _repo.SaveChangesAsync();
            _logger.LogInformation("Order {OrderId} created successfully", order.OrderId);

            return order;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting order {OrderId}", id);
            await _repo.DeleteAsync(id);
            var changed = await _repo.SaveChangesAsync();

            // Remove cache safely
            await _cache.RemoveAsync($"order:{id}");
            _logger.LogInformation("Order {OrderId} deleted, DB changes: {Changes}", id, changed);

            return changed > 0;
        }
    }
}
