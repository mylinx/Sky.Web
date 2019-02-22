using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

namespace Sky.Common
{

    /// <summary>
    /// 缓存接口实现
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        protected IMemoryCache _cache;
      
        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }


        public bool Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }

            if (value==null)
            {
                throw new ArgumentNullException();
            }
            if (!Exists(key))
            {
                _cache.Set(key, value);
            }
            return Exists(key);
        }

        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            object cache;
            return _cache.TryGetValue(key, out cache);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            object cache;
            bool flt = false;
            await Task.Run(() =>
            {
                flt = _cache.TryGetValue(key, out cache);
            });
            return flt;
        }

        public T Get<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }

            return _cache.Get(key);
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            _cache.Remove(key);
            return true;
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
