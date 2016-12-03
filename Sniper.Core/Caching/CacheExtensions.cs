using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Caching
{
    public static class CacheExtensions
    {
        /// <summary>
        /// 获取数据并设置缓存 默认缓存时间60分钟
        /// 当缓存不存在是执行 acquire方法获取数据并设置缓存。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public static T get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// 获取数据并设置缓存。
        /// 当缓存不存在是执行 acquire方法获取数据并设置缓存。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="cacheTime"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public static T get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.isSet(key))
            {
                return cacheManager.get<T>(key);
            }
            var result = acquire();
            if (result != null && cacheTime > 0)
                cacheManager.set(key, result, cacheTime);
            return result;
        }
    }
}
