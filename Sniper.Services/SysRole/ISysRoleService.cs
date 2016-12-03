using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysRole;

namespace Sniper.Services.SysRole
{
    public interface ISysRoleService
    {
        /// <summary>
        /// 获取所有的roles数据
        /// 并缓存
        /// </summary>
        /// <returns></returns>
        IEnumerable<SysRoleMapping> getAllRoles();

        /// <summary>
        /// 从数据库获取所有的角色数据
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        IEnumerable<SysRoleMapping> searchRoles(AdminSearchRoleArg arg);

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Entities.SysRole getRole(Guid id);

        /// <summary>
        /// 保存新增角色
        /// </summary>
        /// <param name="role"></param>
        void InserRole(SysRoleMapping role);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        void DeleteRole(Guid roleId);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        void UpdateRole(SysRoleMapping role);

    }
}
