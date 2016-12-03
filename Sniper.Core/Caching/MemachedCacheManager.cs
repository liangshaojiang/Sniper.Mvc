using Memcached.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class MemachedCacheManager : ICacheManager
    {
        private MemcachedClient _memcachedClient;

        public MemachedCacheManager()
        {
            this._memcachedClient = MemachedManager.getClient();
        }

        /// <summary>
        /// 清理所有缓存
        /// </summary>
        public void clear()
        {
            _memcachedClient.FlushAll();
        }

        /// <summary>
        /// 获取缓存的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T get<T>(string key)
        {
            return (T)_memcachedClient.Get(key);
        }

        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool isSet(string key)
        {
            return _memcachedClient.KeyExists(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void remove(string key)
        {
            _memcachedClient.Delete(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">数据</param>
        /// <param name="cacheTime">分钟</param>
        public void set(string key, object data, int cacheTime)
        {
            _memcachedClient.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
        }


    }
}
