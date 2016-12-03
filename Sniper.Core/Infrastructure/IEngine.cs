using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Infrastructure
{
    public interface IEngine
    {   
        /// <summary>
        /// 初始化方法，需重写此方法
        /// </summary>   
        void Initialize();

        /// <summary>
        /// 构造实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// 批量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();
    }
}
