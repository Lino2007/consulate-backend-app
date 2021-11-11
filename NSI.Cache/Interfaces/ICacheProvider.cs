using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Cache.Interfaces
{
    public interface ICacheProvider
    {
        T Get<T>(string cacheKey);
        T Set<T>(string cacheKey, T item, MemoryCacheEntryOptions cacheOptions = null);
        void Evict(string cacheKey);
    }
}
