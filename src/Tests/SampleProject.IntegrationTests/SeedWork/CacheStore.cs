using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using SampleProject.Infrastructure.Caching;

namespace SampleProject.IntegrationTests.SeedWork
{
    public class CacheStore : ICacheStore
    {
        private IDictionary dictionary = new ListDictionary();
        public void Add<TItem>(TItem item, ICacheKey<TItem> key, TimeSpan? expirationTime = null)
        {
            dictionary.Add(key, item);
        }

        public void Add<TItem>(TItem item, ICacheKey<TItem> key, DateTime? absoluteExpiration = null)
        {
            dictionary.Add(key, item);
        }

        public TItem Get<TItem>(ICacheKey<TItem> key) where TItem : class
        {
            return dictionary[key] as TItem;
        }

        public void Remove<TItem>(ICacheKey<TItem> key)
        {
            dictionary.Remove(key);
        }
    }
}