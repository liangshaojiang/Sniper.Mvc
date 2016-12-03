using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sniper.Mapping.SysUser;

namespace Sniper.Admin.Framework.Security
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 进入系统
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent">记住我</param>
        void signIn(Entities.SysUser user,bool isPersistent);

        /// <summary>
        /// 退出系统
        /// </summary>
        void signOut();

        /// <summary>
        /// 获取验证通过已登录的用户信息
        /// </summary>
        /// <returns></returns>
        SysUserMapping getAuthenticatedSysUser();

        /// <summary>
        /// 判断是否存在登录票据
        /// </summary>
        /// <returns></returns>
        bool isAuthenticated();

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        bool authorize(ActionExecutingContext filterContext);
        
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="action">方法</param>
        /// <param name="controller">控制器</param>
        /// <returns></returns>
        bool authorize(string action, string controller);

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="sysResource">方法资源唯一标示</param>
        /// <returns></returns>
        bool authorize(string sysResource);
    }
}
