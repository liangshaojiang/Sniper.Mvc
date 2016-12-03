using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MemcachedConfigInfo: ConfigurationSection
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static MemcachedConfigInfo getConfig()
        {
            return (MemcachedConfigInfo)ConfigurationManager.GetSection("MemcachedConfigInfo");
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("ServicesList", DefaultValue ="127.0.0.1",IsRequired =true)]
        public string ServicesList
        {
            get { return base["ServicesList"].ToString(); }
            set { base["ServicesList"] = value; }
        }

        /// <summary>
        /// 连接池初始数目
        /// </summary>
        [ConfigurationProperty("InitConnections", DefaultValue = 3, IsRequired = false)]
        public int InitConnections
        {
            get { return (int)base["InitConnections"]; }
            set { base["InitConnections"] = value; }
        }

        /// <summary>
        /// 最小连接数
        /// </summary>
        [ConfigurationProperty("MinConnections", DefaultValue = 3, IsRequired = false)]
        public int MinConnections
        {
            get { return (int)base["MinConnections"]; }
            set { base["MinConnections"] = value; }
        }

        /// <summary>
        /// 最大连接数
        /// </summary>
        [ConfigurationProperty("MaxConnections", DefaultValue = 5, IsRequired = false)]
        public int MaxConnections
        {
            get { return (int)base["MaxConnections"]; }
            set { base["MaxConnections"] = value; }
        }

        /// <summary>
        /// 连接的套接字超时
        /// </summary>
        [ConfigurationProperty("SocketConnectTimeout", DefaultValue = 1000, IsRequired = false)]
        public int SocketConnectTimeout
        {
            get { return (int)base["SocketConnectTimeout"]; }
            set { base["SocketConnectTimeout"] = value; }
        }

        /// <summary>
        /// 套接字超时读取
        /// </summary>
        [ConfigurationProperty("SocketTimeout", DefaultValue = 3000, IsRequired = false)]
        public int SocketTimeout
        {
            get { return (int)base["SocketTimeout"]; }
            set { base["SocketTimeout"] = value; }
        }

        /// <summary>
        /// 维护线程运行的睡眠时间
        /// </summary>
        [ConfigurationProperty("MaintenanceSleep", DefaultValue = (long)30, IsRequired = false)]
        public long MaintenanceSleep
        {
            get { return (long)base["MaintenanceSleep"]; }
            set { base["MaintenanceSleep"] = value; }
        }

        /// <summary>
        /// 获取或设置池的故障标志
        /// </summary>
        [ConfigurationProperty("Failover", DefaultValue = true, IsRequired = false)]
        public bool Failover
        {
            get { return (bool)base["Failover"]; }
            set { base["Failover"] = value; }
        }

        /// <summary>
        /// 如果为false，对所有创建的套接字关闭Nagle的算法
        /// </summary>
        [ConfigurationProperty("Nagle", DefaultValue = false, IsRequired = false)]
        public bool Nagle
        {
            get { return (bool)base["Nagle"]; }
            set { base["Nagle"] = value; }
        }


    }
}
