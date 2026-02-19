using System.Text.Json;
using BlogPlantasDomesticas.Api.Shared.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace BlogPlantasDomesticas.Api.Cache;

public class CacheService : ICacheService
{
    
    private readonly ILogger<CacheService> _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly IDistributedCache _cache;
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(30);

    public CacheService(ILogger<CacheService> logger, IConnectionMultiplexer redis, IDistributedCache distributedCache)
    {
        _logger = logger;
        _redis = redis;
        _cache = distributedCache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var data = await _cache.GetStringAsync(key, cancellationToken);

            if (data == null)
            {
                _logger.LogDebug("No data found for key {key}", key);
                return default;
            }
            
            _logger.LogDebug("Data found for key {key}", key);
            return default;

        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error getting data for key {key}", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
            };

            var data = JsonSerializer.Serialize(value);
            
            await _cache.SetStringAsync(key, data, options, cancellationToken);
            
            _logger.LogDebug("Data set for key {key}", key);

        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error setting data for key {key}", key);
        }
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            await _cache.RemoveAsync(key, cancellationToken);
            _logger.LogDebug("Data removed for key {key}", key);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error removing data for key {key}", key);
        }
    }

    public async Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        try
        {

            var endpoints = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoints.First());
            var keys = server.Keys(pattern: pattern).ToArray();

            if (keys.Length == 0) return;

            var db = _redis.GetDatabase();

            await db.KeyDeleteAsync(keys);
            
            _logger.LogDebug("Data removed for key {key}", keys);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error removing pattern: {Pattern}", pattern);
        }
    }
}