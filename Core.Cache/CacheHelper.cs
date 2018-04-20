using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Core.Utility
{
    public class CacheHelper:ICacheHelper
    {
        private IMemoryCache _cache;

        public CacheHelper(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public object TryGetValueSet(string name, out object cacheEntry)
        {
            if (!_cache.TryGetValue(name, out cacheEntry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20));
                _cache.Set(name, cacheEntry, cacheEntryOptions);

            }
            return cacheEntry;
        }

        public async Task<object> GetOrCreateAsync(string name,object cacheEntry)
        {
            var cacheEntryResult = await _cache.GetOrCreateAsync(name, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(20);
                return Task.FromResult(cacheEntry);
            });
            return cacheEntryResult;
        }

        public object GetOrCreate(string name,object cacheEntry)
        {
            var cacheEntryResult = _cache.GetOrCreate(name, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(20);
                return cacheEntry;
            });
            return cacheEntryResult;
        }

    }
}
