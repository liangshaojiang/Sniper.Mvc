using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core;
using Sniper.Core.Caching;
using Sniper.Core.Data;
using Sniper.Entities;
using Sniper.Mapping.SysUser;
using Sniper.Services.ActivityLog;
using System.Data.Entity;
using Sniper.Core.Librs;
using Sniper.Mapping;

namespace Sniper.Services.SysUser
{
    public class SysUserService : ISysUserService
    {
        private const string MODEL_KEY = "Sniper.admin.user_{0}";

        private IRepository<Entities.SysUser> _sysUserRepository;
        private IActivityLogService _activityLogService;
        private ICacheManager _cacheManager;

        public SysUserService(IRepository<Entities.SysUser> sysUserRepository,
            IActivityLogService activityLogService,
            ICacheManager cacheManager)
        {
            this._sysUserRepository = sysUserRepository;
            this._activityLogService = activityLogService;
            this._cacheManager = cacheManager;
        }

        /// <summary>
        /// 通过账号获取单条数据，只能存在一条或0条
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Entities.SysUser getUserByAccount(string account)
        {
            return _sysUserRepository.Table.SingleOrDefault(o => o.Account == account && !o.IsDeleted);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entities.SysUser getUserById(Guid id)
        {
            return _sysUserRepository.Table.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        public void updateSysUser(Entities.SysUser entity)
        {
            var user = getUserById(entity.Id);
            if (user == null)
                return;
            user.Name = entity.Name;
            user.Sex = entity.Sex;
            user.MobilePhone = entity.MobilePhone;
            user.Email = entity.Email;
            user.ModifiedTime = DateTime.Now;
            user.Modifier = entity.Modifier;
            _sysUserRepository.update(user);
            _activityLogService.entityUpdated(user);
            removeCacheUser(entity.Id);
        }

        /// <summary>
        /// 通过id获取已登录用户的数据
        /// 获取后并缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUserMapping getLoggedUserById(Guid userId)
        {
            string key = String.Format(MODEL_KEY, userId);
            return _cacheManager.get<SysUserMapping>(key, () =>
            {
                var item = _sysUserRepository.getById(userId);
                if (item != null)
                    return item.toModel();
                return null;
            });
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="entity"></param>
        public void insertSysUser(Entities.SysUser entity)
        {
            if (existAccount(entity.Account))
                return;
            _sysUserRepository.insert(entity);
            _activityLogService.entityInserted(entity);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        public void resetPassword(Guid id,Guid modifer)
        {
            var item = getUserById(id);
            if (item == null)
                return;
            if (item.IsAdmin)
                return;
            item.Modifier = modifer;
            item.ModifiedTime = DateTime.Now;
            item.Password = Core.Encyption.EncryptorHelper.GetMD5(item.Account + item.Salt);
            _sysUserRepository.update(item);
            _activityLogService.entityInserted(item);
            removeCacheUser(id);
        }

        /// <summary>
        /// 管理查询用户
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IPagedList<Entities.SysUser> searchUser(SysUserSearchArg arg, int page, int size)
        {
            var query = _sysUserRepository.Table.Where(o => !o.IsDeleted);
            if (arg != null)
            {
                if (!String.IsNullOrEmpty(arg.q))
                    query = query.Where(o => o.Account.Contains(arg.q) || o.MobilePhone.Contains(arg.q) || o.Email.Contains(arg.q) || o.Name.Contains(arg.q));
                if (arg.enabled.HasValue)
                    query = query.Where(o => o.Enabled == arg.enabled);
                if (arg.unlock.HasValue)
                    query = query.Where(o => o.LoginLock == arg.unlock);
                if (arg.roleId.HasValue)
                    query = query.Where(o => o.SysUserRole.Any(r => r.RoleId == arg.roleId));
            }
            query = query.OrderBy(o => o.Account).ThenByDescending(o => o.CreationTime).OrderBy(o => o.Name);
            return new PagedList<Entities.SysUser>(query, page, size);
        }

        /// <summary>
        /// 验证账号是否已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool existAccount(string account)
        {
            return _sysUserRepository.Table.Any(o => o.Account == account && !o.IsDeleted);
        }

        /// <summary>
        /// 启用禁用账号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <param name="modifer"></param>
        public void enabled(Guid id, bool enabled, Guid modifer)
        {
            var user = getUserById(id);
            if (user == null)
                return;
            if (user.IsAdmin)
                return;
            user.Enabled = enabled;
            user.Modifier = modifer;
            user.ModifiedTime = DateTime.Now;
            _sysUserRepository.update(user);
            _activityLogService.entityUpdated(user);
            removeCacheUser(id);
        }

        /// <summary>
        /// 登录锁解锁
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ulock"></param>
        /// <param name="modifer"></param>
        public void loginLock(Guid id, bool ulock, Guid modifer)
        {
            var user = getUserById(id);
            if (user == null)
                return;
            user.LoginLock = ulock;
            user.Modifier = modifer;
            user.ModifiedTime = DateTime.Now;
            _sysUserRepository.update(user);
            _activityLogService.entityUpdated(user);
            removeCacheUser(id);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        public void deleteUser(Guid id, Guid modifer)
        {
            var user = getUserById(id);
            if (user == null)
                return;
            if (user.IsAdmin)
                return;
            user.DeletedTime = DateTime.Now;
            user.IsDeleted = true;
            user.Modifier = modifer;
            _sysUserRepository.update(user);
            _activityLogService.entityDeleted(user);
            removeCacheUser(id);
        }

        /// <summary>
        /// 保存上传的头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="avatar"></param>
        public void addAvatar(Guid id, byte[] avatar)
        {
           var user = getUserById(id);
            if (user == null)
                return;
            user.Avatar = avatar;
            user.Modifier = id;
            user.ModifiedTime = DateTime.Now;
            _sysUserRepository.update(user);
            _activityLogService.entityUpdated(user);
            removeCacheUser(id);
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public void changePassword(Guid id, string password)
        {
            var user = getUserById(id);
            if (user == null)
                return;
            user.Password = password;
            user.ModifiedTime = DateTime.Now;
            user.Modifier = id;
            _sysUserRepository.update(user);
            _activityLogService.entityUpdated(user);
            removeCacheUser(id);
        }

        /// <summary>
        /// 移除缓存用户
        /// </summary>
        /// <param name="userId"></param>
        private void removeCacheUser(Guid userId)
        {
            _cacheManager.remove(String.Format(MODEL_KEY, userId));
        }

        /// <summary>
        /// 设置最后活动时间
        /// </summary>
        /// <param name="id"></param>
        public void lastActivityTime(Guid id)
        {
           var user = getUserById(id);
            if (user == null)
                return;
            user.LastActivityTime = DateTime.Now;
            _sysUserRepository.update(user);
            removeCacheUser(id);
        }
    }
}
