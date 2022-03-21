using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Redis_WebApi.Controllers;

namespace Redis_WebApi;

public static class IDistributedCacheExtensions
{
    public static async Task<GetCachedValue<T>> GetOrAddAsync<T, TKey>(
        this IDistributedCache cache,
        TKey anyKey,
        Func<TKey, Task<T>> factory)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(anyKey);
        var key = anyKey switch
        {
            string k => k,
            _ => anyKey.ToString(),
        };

        var jsonValue = await cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(jsonValue))
        {
           var value = await factory(anyKey);
            await cache.SetStringAsync(key, JsonSerializer.Serialize(value));
            return new(false, value);
        }

        return new(true, JsonSerializer.Deserialize<T>(jsonValue));
    }


    public static async Task<long> GetLongAsync(this IDistributedCache cache, string key)
    {
        var value = await cache.GetAsync(key);
        return BitConverter.ToInt64(value);
    }

    public static Task SetLongAsync(this IDistributedCache cache, string key, long value)
    {
        return cache.SetAsync(key, BitConverter.GetBytes(value));
    }

    public static async Task<DateTime> GetDateTimeAsync(this IDistributedCache cache, string key)
    {
        var value = await cache.GetAsync(key);
        var ticks = BitConverter.ToInt64(value);
        return new(ticks);
    }

    public static Task SetDateTimeAsync(this IDistributedCache cache, string key, DateTime value)
    {
        var ticks = value.Ticks;
        return cache.SetAsync(key, BitConverter.GetBytes(ticks));
    }

    public static Task SetAsync<T>(this IDistributedCache cache, T value)
        where T : ServiceThree.ICacheKey
    {
        return cache.SetStringAsync(value.CacheKey, JsonSerializer.Serialize(value));
    }

    public record GetCachedValue<T>(bool Cached, T value);
}

