using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysRole;
using Sniper.Mapping.SysUserRole;

namespace Sniper.Services.SysUserRole
{
    public interface ISysUserRoleService
    {
        /// <summary>
        /// 获取用户的角色数据
        /// 获取后并缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<SysRoleMapping> getRoleByUserId(Guid userId);

        /// <summary>
        /// 保存设置的用户角色数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        void SaveUserRole(Guid userId,IEnumerable<Guid> roleIds);

    }
}
