using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysPermission;

namespace Sniper.Services.SysPermission
{
    public interface ISysPermissionService
    {
        /// <summary>
        /// 获取所有的数据并缓存，不存在返回null
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionRecordMapping> getAllPermissionRecordes();

        /// <summary>
        /// 通过roleid获取
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<PermissionRecordMapping> getByRoleId(Guid roleId);

        /// <summary>
        /// 通过roleids获取
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        IEnumerable<PermissionRecordMapping> getByRoleIds(IEnumerable<Guid> roleIds);

        /// <summary>
        /// 保存设置角色的权限数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sysResource"></param>
        /// <param name="creator"></param>
        void SavePermissionRecord(Guid roleId,IEnumerable<string> sysResource,Guid creator);
    }
}
