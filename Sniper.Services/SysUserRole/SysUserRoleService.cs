using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Caching;
using Sniper.Core.Data;
using Sniper.Mapping.SysRole;
using Sniper.Mapping.SysUserRole;
using Sniper.Services.SysRole;

namespace Sniper.Services.SysUserRole
{
    public class SysUserRoleService : ISysUserRoleService
    {
        private const string MODEL_KEY = "Sniper.admin.user.role_all";

        private ICacheManager _cacheManager;
        private IRepository<Entities.SysUserRole> _sysUserRoleRepository;
        private ISysRoleService _sysRoleService;

        public SysUserRoleService(ICacheManager cacheManager,
             IRepository<Entities.SysUserRole> sysUserRoleRepository,
             ISysRoleService sysRoleService)
        {
            this._cacheManager = cacheManager;
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 获取所有的用户角色数据，
        /// 并缓存
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SysUserRoleMapping> getAllUserRoles()
        {
            if (_cacheManager.isSet(MODEL_KEY))
                return _cacheManager.get<List<SysUserRoleMapping>>(MODEL_KEY);
            var list = _sysUserRoleRepository.Table.ToList();
            if (list != null && list.Any())
            {
                var result = Core.Librs.MapperManager.MaList<Entities.SysUserRole, SysUserRoleMapping>(list);
                _cacheManager.set(MODEL_KEY, result, 60);
                return result;
            }
            return null;
        }

        /// <summary>
        /// 获取用户的角色列表数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<SysRoleMapping> getRoleByUserId(Guid userId)
        {
            var roles = _sysRoleService.getAllRoles();
            if (roles == null || !roles.Any())
                return null;
            var list = getAllUserRoles();
            if (list == null || !list.Any())
                return null;
            return list.Join(roles, a => a.RoleId, b => b.Id, (a, b) => b).ToList();
        }

        /// <summary>
        /// 保存用户角色设置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        public void SaveUserRole(Guid userId, IEnumerable<Guid> roleIds)
        {
            var list = _sysUserRoleRepository.Table.Where(o => o.UserId == userId).ToList();
            if (roleIds == null || !roleIds.Any())
                foreach (var del in list)
                    _sysUserRoleRepository.Entities.Remove(del);
            else
            {
                foreach (Guid roleId in roleIds)
                    if (!list.Any(o => o.RoleId == roleId))
                        _sysUserRoleRepository.Entities.Add(new Entities.SysUserRole() { Id = Guid.NewGuid(), RoleId = roleId, UserId = userId });
                foreach (var del in list)
                    if (!roleIds.Any(o => o == del.RoleId))
                        _sysUserRoleRepository.Entities.Remove(del);
            }
            _sysUserRoleRepository.DbContext.SaveChanges();
            _cacheManager.remove(MODEL_KEY);
        }
    }
}
