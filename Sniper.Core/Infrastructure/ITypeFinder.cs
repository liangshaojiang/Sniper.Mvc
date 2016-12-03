using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Infrastructure
{
    public interface ITypeFinder
    {
        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns></returns>
        IList<Assembly> GetAssemblies();

        /// <summary>
        /// 查找类型集合，不支持泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>();

    }
}
