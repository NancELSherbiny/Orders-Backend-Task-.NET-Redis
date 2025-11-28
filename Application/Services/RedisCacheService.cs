using Application.Interfaces;
using StackExchange.Redis;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _db;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(IConnectionMultiplexer redis, ILogger<RedisCacheService> logger)
        {
            _db = redis.GetDatabase();
            _logger = logger;
        }

        public async Task<string?> GetAsync(string key)
        {
            try
            {
                var value = await _db.StringGetAsync(key);
                if (value.IsNullOrEmpty) { _logger.LogInformation("Cache miss for {Key}", key); return null; }
                _logger.LogInformation("Cache hit for {Key}", key);
                return value.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis unavailable when getting {Key} — falling back to DB", key);
                return null; // fail open: behave like cache miss
            }
        }


        public async Task SetAsync(string key, string value, TimeSpan ttl)
        {
            _logger.LogInformation("Setting cache {Key} with TTL {TTL}", key, ttl);
            await _db.StringSetAsync(key, value, ttl);
        }

        public async Task RemoveAsync(string key)
        {
            _logger.LogInformation("Removing cache {Key}", key);
            await _db.KeyDeleteAsync(key);
        }
    }
}
