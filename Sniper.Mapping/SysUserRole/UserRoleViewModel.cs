using Sniper.Mapping.SysRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysUserRole
{
    /// <summary>
    /// 设置用户角色对象
    /// </summary>
    [Serializable]
    public class UserRoleViewModel
    {
        public Entities.SysUser SysUser { get; set; }

        /// <summary>
        /// 所有的角色
        /// </summary>
        public IEnumerable<SysRoleMapping> RoleList { get; set; }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        public IEnumerable<SysRoleMapping> UserRoleList { get; set; }
    }
}
