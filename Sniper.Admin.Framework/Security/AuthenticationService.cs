using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sniper.Admin.Framework.Menu;
using Sniper.Core.Caching;
using Sniper.Core.Librs;
using Sniper.Entities;
using Sniper.Mapping.SysUser;
using Sniper.Services.SysPermission;
using Sniper.Services.SysUser;
using Sniper.Services.SysUserRole;
using Sniper.Services.SysUserToken;

namespace Sniper.Admin.Framework.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private ISysUserTokenService _sysUserTokenService;
        private ISysUserRegistrationService _sysUserRegistrationService;
        private ICacheManager _cacheManager;
        private ISysUserService _sysUserService;
        private ISysUserRoleService _sysUserRoleService;
        private ISysPermissionService _sysPermissionService;

        public AuthenticationService(ISysUserTokenService sysUserTokenService,
            ISysUserRegistrationService sysUserRegistrationService,
            ISysUserService sysUserService,
            ICacheManager cacheManager,
            ISysPermissionService sysPermissionService,
            ISysUserRoleService sysUserRoleService)
        {
            this._sysUserTokenService = sysUserTokenService;
            this._sysUserRegistrationService = sysUserRegistrationService;
            this._cacheManager = cacheManager;
            this._sysUserService = sysUserService;
            this._sysUserRoleService = sysUserRoleService;
            this._sysPermissionService = sysPermissionService;
        }

        /// <summary>
        /// 进入系统
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        public void signIn(SysUser user, bool isPersistent)
        {
            Entities.SysUserToken userToken = new Entities.SysUserToken()
            {
                Id = Guid.NewGuid(),
                SysUserId = user.Id,
                ExpireTime = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            _sysUserTokenService.insertToken(userToken);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                user.Account,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent,
                userToken.Id.ToString(),
                FormsAuthentication.FormsCookiePath);
            string ticketValue = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketValue);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
                cookie.Domain = FormsAuthentication.CookieDomain;
            CookieHelper.Add(cookie);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void signOut()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 获取验证通过已登录的用户信息
        /// </summary>
        /// <returns></returns>
        public SysUserMapping getAuthenticatedSysUser()
        {
            if (!isAuthenticated())
                return null;
            var identity = (FormsIdentity)HttpContext.Current.User.Identity;
            return GetAuthenticatedCustomerFromTicket(identity.Ticket);
        }

        /// <summary>
        /// 获取已登录的用户信息
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        private SysUserMapping GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            string token_id = ticket.UserData;
            if (String.IsNullOrEmpty(token_id))
                throw new Exception("ticket.UserData 为空");
            Guid tokenId = Guid.Empty;
            if (Guid.TryParse(token_id, out tokenId))
            {
                var token_item = _sysUserTokenService.getLoggedToken(tokenId);
                if (token_item != null)
                    return _sysUserService.getLoggedUserById(token_item.SysUserId);
            }
            return null;
        }

        /// <summary>
        /// 判断是否存在登录票据
        /// </summary>
        /// <returns></returns>
        public bool isAuthenticated()
        {
            return HttpContext.Current != null
                && HttpContext.Current.Request != null
                && HttpContext.Current.Request.IsAuthenticated
                && (HttpContext.Current.User.Identity is FormsIdentity);
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool authorize(ActionExecutingContext filterContext)
        {
            string controller = (string)filterContext.RouteData.Values["controller"];
            string action = (string)filterContext.RouteData.Values["action"];
            return authorize(action, controller);
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public bool authorize(string action, string controller)
        {
            if (ReflectManager.Instance.Descripers == null || !ReflectManager.Instance.Descripers.Any())
                return true;
            var descriper = ReflectManager.Instance.Descripers.FirstOrDefault(o => action.Equals(o.Action, StringComparison.InvariantCultureIgnoreCase)
                            && controller.Equals(o.Controller, StringComparison.InvariantCultureIgnoreCase));
            return authorize(descriper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysResource"></param>
        /// <returns></returns>
        public bool authorize(string sysResource)
        {
            if (String.IsNullOrEmpty(sysResource))
                return false;
            if (ReflectManager.Instance.Descripers == null || !ReflectManager.Instance.Descripers.Any())
                return true;
            var descriper = ReflectManager.Instance.Descripers.FirstOrDefault(o=>sysResource.Equals(o.SysResource,StringComparison.InvariantCultureIgnoreCase));
            return authorize(descriper);
        }
        /// <summary>
        /// 私有方法，判断权限
        /// </summary>
        /// <param name="descriper"></param>
        /// <returns></returns>
        private bool authorize(DescriperAttribute descriper)
        {
            //未加入控制
            if (descriper == null)
                return true;
            //获取登录用户
            var user = getAuthenticatedSysUser();
            if (user == null)
                return false;
            //超级管理员
            if (user.IsAdmin)
                return true;
            //获取角色
            var list = _sysUserRoleService.getRoleByUserId(user.Id);
            if (list == null || !list.Any())
                return false;
            //角色权限
            var perm_list = _sysPermissionService.getByRoleIds(list.Select(x => x.Id).ToList());
            if (perm_list == null || !perm_list.Any())
                return false;
            return perm_list.Any(o => o.SysResource.Equals(descriper.SysResource, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
