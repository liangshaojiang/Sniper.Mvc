using Sniper.Mapping.SysStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Services.SysStore
{
    public interface ISysStoreService
    {
        /// <summary>
        /// 保存系统名称设置
        /// </summary>
        /// <param name="model"></param>
        void saveStore(SysStoreMapping model);

        /// <summary>
        /// 获取系统名称对象
        /// </summary>
        /// <returns></returns>
        SysStoreMapping getStore();
    }
}
