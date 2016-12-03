using Sniper.Mapping.SysPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sniper.Mapping.SysRole
{
    [Serializable]
    public class RolePermissionViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public SysRoleMapping Role { get; set; }

        /// <summary>
        /// 角色select下拉菜单
        /// </summary>
        public IEnumerable<SysRoleMapping> RoleList { get; set; }

        /// <summary>
        /// 角色的权限数据
        /// </summary>
        public IEnumerable<PermissionRecordMapping> Permissions { get; set; }
    }
}
