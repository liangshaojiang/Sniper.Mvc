using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysUser;
using Sniper.Mapping.SysStore;

namespace Sniper.Admin.Framework.Infrastructure
{
    public interface IWorkContext
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        SysUserMapping CurrentUser { get; }

        /// <summary>
        /// 系统对象
        /// </summary>
        SysStoreMapping Store { get; }
         

    }
}
