using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace SampleProject.Infrastructure.Caching
{
    public class MemoryCacheStore : ICacheStore
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<string, TimeSpan> _expirationConfiguration;

        public MemoryCacheStore(
            IMemoryCache memoryCache,
            Dictionary<string, TimeSpan> expirationConfiguration)
        {
            _memoryCache = memoryCache;
            this._expirationConfiguration = expirationConfiguration;
        }

        public void Add<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null)
        {
            var cachedObjectName = item.GetType().Name;
            TimeSpan timespan;
            if (expirationTime.HasValue)
            {
                timespan = expirationTime.Value;
            }
            else
            {
                timespan = _expirationConfiguration[cachedObjectName];
            }

            this._memoryCache.Set(key.CacheKey, item, timespan);
        }

        public void Add<TItem>(TItem item, ICacheKey<TItem> key, DateTime? absoluteExpiration = null)
        {
            DateTimeOffset offset;
            if (absoluteExpiration.HasValue)
            {
                offset = absoluteExpiration.Value;
            }
            else
            {
                offset = DateTimeOffset.MaxValue;
            }

            this._memoryCache.Set(key.CacheKey, item, offset);
        }

        public TItem Get<TItem>(ICacheKey<TItem> key) where TItem : class
        {
            if (this._memoryCache.TryGetValue(key.CacheKey, out TItem value))
            {
                return value;
            }

            return null;
        }

        public void Remove<TItem>(ICacheKey<TItem> key)
        {
            this._memoryCache.Remove(key.CacheKey);
        }
    }
}