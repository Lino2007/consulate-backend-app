using Microsoft.Extensions.Caching.Memory;
using NSI.Cache.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Cache.Implementations
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string cacheKey)
        {
            var item = (T)_memoryCache.Get(cacheKey);
            return item;
        }

        public T Set<T>(string cacheKey, T item, MemoryCacheEntryOptions cacheOptions = null)
        {
            if (item == null)
            {
                _memoryCache.Remove(cacheKey);
                return default;
            }

            return _memoryCache.Set(cacheKey, item, cacheOptions);
        }

        public void Evict(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}
