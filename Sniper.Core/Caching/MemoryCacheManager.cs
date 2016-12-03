using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Caching
{
    public class MemoryCacheManager:ICacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }



        /// <summary>
        /// 清理缓存
        /// </summary>
        public void clear()
        {
            foreach (var item in Cache)
                remove(item.Key);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool isSet(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public void set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Set(new CacheItem(key, data), policy);
        }
    }
}
