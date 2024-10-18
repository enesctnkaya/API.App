using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task AddAsync<T>(string cachekey, T value, TimeSpan exprTimeSpan)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = exprTimeSpan
            };

            memoryCache.Set(cachekey, value, cacheOptions);

            return Task.CompletedTask;

        }

        public Task<T?> GetAsync<T>(string cachekey)
        {
            return Task.FromResult(memoryCache.TryGetValue(cachekey, out T cacheItem) ? cacheItem : default(T));
        }

        public Task RemoveAsync(string cachekey)
        {
            memoryCache.Remove(cachekey);

            return Task.CompletedTask;
        }
    }
}
