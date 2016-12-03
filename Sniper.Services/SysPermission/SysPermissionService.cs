using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Caching;
using Sniper.Core.Data;
using Sniper.Core.Librs;
using Sniper.Mapping.SysPermission;
using Sniper.Mapping;

namespace Sniper.Services.SysPermission
{
    public class PermissionRecordService : ISysPermissionService
    {
        private const string MODEL_KEY = "Sniper.admin.permission_all";
        private ICacheManager _cacheManager;
        private IRepository<Entities.SysPermission> _sysPermissionRepository;

        public PermissionRecordService(ICacheManager cacheManager,
            IRepository<Entities.SysPermission> sysPermissionRepository)
        {
            this._cacheManager = cacheManager;
            this._sysPermissionRepository = sysPermissionRepository;
        }

        /// <summary>
        /// 获取所有的数据并缓存，不存在返回null
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PermissionRecordMapping> getAllPermissionRecordes()
        {
            if (_cacheManager.isSet(MODEL_KEY))
                return _cacheManager.get<List<PermissionRecordMapping>>(MODEL_KEY);
            var list = _sysPermissionRepository.Table.ToList();
            if (list != null && list.Any())
            {
                var result = list.Select(x => x.toModel()).ToList();
                _cacheManager.set(MODEL_KEY, result, 60);
                return result;
            }
            return null;
        }

        /// <summary>
        ///  通过roleid获取
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<PermissionRecordMapping> getByRoleId(Guid roleId)
        {
            var list = getAllPermissionRecordes();
            if (list != null && list.Any())
                return list.Where(o => o.RoleId == roleId).ToList();
            return null;
        }

        /// <summary>
        /// 通过roleids获取
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public IEnumerable<PermissionRecordMapping> getByRoleIds(IEnumerable<Guid> roleIds)
        {
            var list = getAllPermissionRecordes();
            if (list != null && list.Any())
                return list.Where(o => roleIds.Contains(o.RoleId)).ToList();
            return null;
        }

        /// <summary>
        /// 保存设置角色的权限数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissions">sysResource</param>
        public void SavePermissionRecord(Guid roleId, IEnumerable<string> sysResource, Guid creator)
        {
            var list = _sysPermissionRepository.Table.Where(o => o.RoleId == roleId);
            if (sysResource == null || !sysResource.Any())
                foreach (var del in list)
                    _sysPermissionRepository.Entities.Remove(del);
            else
            {
                foreach (string resource in sysResource)
                {
                    var item = list.FirstOrDefault(o => o.SysResource == resource);
                    if (item == null)
                        _sysPermissionRepository.Entities.Add(new Entities.SysPermission()
                        {
                            Id = Guid.NewGuid(),
                            RoleId = roleId,
                            CreationTime = DateTime.Now,
                            Creator = creator,
                            SysResource = resource
                        });
                }
                foreach (var del in list)
                    if (!sysResource.Any(o => o == del.SysResource))
                        _sysPermissionRepository.Entities.Remove(del);
            }
            _sysPermissionRepository.DbContext.SaveChanges();
            _cacheManager.remove(MODEL_KEY);
        }
    }
}
