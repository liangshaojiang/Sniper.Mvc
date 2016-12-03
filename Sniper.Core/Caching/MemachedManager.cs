using Memcached.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Caching
{
    /// <summary>
    /// 管理
    /// </summary>
    public sealed class MemachedManager
    {
        //配置信息
        private static readonly MemcachedConfigInfo config = MemcachedConfigInfo.getConfig();
         
        private static MemcachedClient _memcachedClient;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static MemachedManager()
        {
            createManager();
        }

        /// <summary>
        /// 私有静态函数
        /// </summary>
        private static void createManager()
        {
            SockIOPool sockIOPool = SockIOPool.GetInstance();
            string[] services = splitString(config.ServicesList, ",");
            sockIOPool.SetServers(services);
            sockIOPool.InitConnections = config.InitConnections;
            sockIOPool.MaxConnections = config.MaxConnections;
            sockIOPool.MinConnections = config.MinConnections;
            sockIOPool.Nagle = config.Nagle;
            sockIOPool.Failover = config.Failover;
            sockIOPool.MaintenanceSleep = config.MaintenanceSleep;
            sockIOPool.SocketTimeout = config.SocketTimeout;
            sockIOPool.SocketConnectTimeout = config.SocketConnectTimeout;

            sockIOPool.Initialize();
            _memcachedClient = new MemcachedClient();
        }

        /// <summary>
        /// 字符串拆分成数组
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        private static string[] splitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }


        /// <summary>
        /// 获取MemcachedClient
        /// </summary>
        /// <returns></returns>
        public static MemcachedClient getClient()
        {
            if (_memcachedClient == null)
            {
                createManager();
            }
            return _memcachedClient;
        }
    }
}
