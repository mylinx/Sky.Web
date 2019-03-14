using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Common
{
    public  class RedisCacheService:ICacheService
    {
        protected RedisCache _redisCache = null;

        public RedisCacheService(RedisCacheOptions options)
        {
            _redisCache = new RedisCache(options);
        }
         

         
        public bool Add(string key, object value,int ExpirtionTime=20)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _redisCache.Set(key, Encoding.UTF8.GetBytes(value.ToString()), new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(ExpirtionTime)
                });
            }
            return true; 
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (Exists(key))
            {
                _redisCache.Remove(key);
                return true;
            } 
            return false;
        }


        public string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            if (Exists(key))
            {
                return _redisCache.GetString(key);
            }
            return null;
        }


        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
          return  !string.IsNullOrEmpty(_redisCache.GetString(key)) ? true :false;
        }
        
        //需要异步执行可自行添加
    }
}
