using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Caching;
using Sniper.Core.Data;
using Sniper.Mapping.SysRole;
using System.Data.Entity;
using Sniper.Entities;
using Sniper.Mapping;
using Sniper.Services.ActivityLog;
using Sniper.Mapping.SysUser;

namespace Sniper.Services.SysRole
{
    public class SysRoleService : ISysRoleService
    {
        private const string MODEL_KEY = "Sniper.admin.role_all";
        private ICacheManager _cacheManager;
        private IRepository<Entities.SysRole> _sysRoleRepository;
        private IRepository<Entities.SysUserRole> _sysUserRoleRepository;
        private IRepository<Entities.SysPermission> _sysPermissionRepository;
        private IActivityLogService _activityLogService;

        public SysRoleService(ICacheManager cacheManager,
             IRepository<Entities.SysRole> sysRoleRepository,
             IRepository<Entities.SysUserRole> sysUserRoleRepository,
             IRepository<Entities.SysPermission> sysPermissionRepository,
             IActivityLogService activityLogService)
        {
            this._cacheManager = cacheManager;
            this._sysRoleRepository = sysRoleRepository;
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysPermissionRepository = sysPermissionRepository;
            this._activityLogService = activityLogService;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        public void DeleteRole(Guid roleId)
        {
           var item = _sysRoleRepository.getById(roleId);
            if (item == null)
                return;
            foreach(var del in item.SysPermission.ToList())
                _sysPermissionRepository.Entities.Remove(del);
            foreach (var del in _sysUserRoleRepository.Table.Where(o => o.RoleId == roleId).ToList())
                _sysUserRoleRepository.Entities.Remove(del);
            _sysRoleRepository.delete(item);
            _activityLogService.entityDeleted(item);
            _cacheManager.remove(MODEL_KEY);
        }

        /// <summary>
        /// 获取所有的roles数据
        /// 并缓存
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysRoleMapping> getAllRoles()
        {
            if (_cacheManager.isSet(MODEL_KEY))
                return _cacheManager.get<List<SysRoleMapping>>(MODEL_KEY);
            var list = _sysRoleRepository.Table.ToList();
            if (list != null && list.Any())
            {
                var result = Core.Librs.MapperManager.MaList<Entities.SysRole, SysRoleMapping>(list);
                _cacheManager.set(MODEL_KEY, result, 60);
                return result;
            }
            return null;
        }
         

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        public void InserRole(SysRoleMapping role)
        {
            var item = role.toEntity();
            _sysRoleRepository.insert(item);
            _activityLogService.entityInserted(item);
            _cacheManager.remove(MODEL_KEY);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        public void UpdateRole(SysRoleMapping role)
        {
            var item = _sysRoleRepository.getById(role.Id);
            if (item == null)
                return;
            item.Name = role.Name;
            item.ModifiedTime = DateTime.Now;
            item.Modifier = role.Modifier; 
            _sysRoleRepository.update(item);
            _activityLogService.entityUpdated(item);
            _cacheManager.remove(MODEL_KEY);
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entities.SysRole getRole(Guid id)
        {
           return _sysRoleRepository.getById(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public IEnumerable<SysRoleMapping> searchRoles(AdminSearchRoleArg arg)
        {
            var query = _sysRoleRepository.Table;
            if(arg!=null)
            {
                if (!String.IsNullOrEmpty(arg.q))
                    query = query.Where(o => o.Name.Contains(arg.q));
            }
            return query.Select(item => new SysRoleMapping()
            {
                Id = item.Id,
                Name = item.Name,
                SysUserName = item.SysUser.Name
            }).ToList();
        }
    }
}
