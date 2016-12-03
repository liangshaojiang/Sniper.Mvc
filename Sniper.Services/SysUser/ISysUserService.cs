using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysUser;
using Sniper.Core;

namespace Sniper.Services.SysUser
{
    public interface ISysUserService
    {
        /// <summary>
        /// 通过账号获取单条数据，只能存在一条或0条
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Entities.SysUser getUserByAccount(string account);

        /// <summary>
        /// 通过id获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Entities.SysUser getUserById(Guid id);

        /// <summary>
        /// 新增系统用户
        /// </summary>
        /// <param name="entity"></param>
        void insertSysUser(Entities.SysUser entity);

        /// <summary>
        /// 更新修改用户资料
        /// </summary>
        /// <param name="entity"></param>
        void updateSysUser(Entities.SysUser entity);

        /// <summary>
        /// 重置密码。默认重置成账号一样
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        void resetPassword(Guid id,Guid modifer);

        /// <summary>
        /// 通过id获取已登录用户的数据
        /// 获取后并缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysUserMapping getLoggedUserById(Guid userId);

        /// <summary>
        /// 查询系统用户列表数据
        /// </summary>
        /// <param name="arg">查询参数</param>
        /// <param name="page">当前页</param>
        /// <param name="size">页条数</param>
        /// <returns></returns>
        IPagedList<Entities.SysUser> searchUser(SysUserSearchArg arg, int page, int size);

        /// <summary>
        /// 验证账号是否已经存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool existAccount(string account);

        /// <summary>
        /// 启用禁用账号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <param name="modifer"></param>
        void enabled(Guid id, bool enabled, Guid modifer);

        /// <summary>
        /// 登录锁与解锁
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ulock"></param>
        /// <param name="modifer"></param>
        void loginLock(Guid id, bool ulock, Guid modifer);

        /// <summary>
        /// 删除用户。无法删除超级用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        void deleteUser(Guid id, Guid modifer);

        /// <summary>
        /// 添加用户头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="avatar"></param>
        void addAvatar(Guid id, byte[] avatar);

        /// <summary>
        /// 用户自己修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        void changePassword(Guid id, string password);

        /// <summary>
        /// 设置用户最后多动时间
        /// </summary>
        /// <param name="id"></param>
        void lastActivityTime(Guid id);
    }
}
