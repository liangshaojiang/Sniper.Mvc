using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Caching
{
    public interface ICacheManager
    { 
        /// <summary>
        /// 获取换成数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T get<T>(string key);
        
        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="data">数据</param>
        /// <param name="cacheTime">缓存时间，分钟</param>
        void set(string key, object data, int cacheTime);
        
        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool isSet(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void remove(string key);
          
        /// <summary>
        /// 清楚所有的缓存
        /// </summary>
        void clear();
    }
}
