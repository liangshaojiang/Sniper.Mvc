using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sniper.Admin.Framework.Security;
using Sniper.Core;

namespace Sniper.Admin.Framework.Mvc.Filters
{
    /// <summary>
    /// 身份验证过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _dontValidate;

        public AdminAuthorizeAttribute() : this(false)
        {

        }

        public AdminAuthorizeAttribute(bool dontValidate)
        {
            this._dontValidate = dontValidate;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        private IEnumerable<AdminAuthorizeAttribute> getAdminAuthorizeAttributes(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true))
                .OfType<AdminAuthorizeAttribute>();
        }

        /// <summary>
        /// 允许匿名访问的特性
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        private bool getAllowAttributes(ActionDescriptor descriptor)
        {
            var allowAttributes = descriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true))
                .OfType<AllowAnonymousAttribute>();
            return allowAttributes != null && allowAttributes.Any();
        }

        /// <summary>
        /// 管理员才能访问的页面
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool isAdminPageRequested(AuthorizationContext filterContext)
        {
            var adminAttributes = getAdminAuthorizeAttributes(filterContext.ActionDescriptor);
            if (adminAttributes != null && adminAttributes.Any())
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext 为空");

            if (_dontValidate)
                return;
            if (getAllowAttributes(filterContext.ActionDescriptor))
                return;

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
                throw new InvalidOperationException("子方法不能添加登录验证特性");
            if (isAdminPageRequested(filterContext))
            {
                var _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
                if (_authenticationService.isAuthenticated())
                {
                    var user = _authenticationService.getAuthenticatedSysUser();
                    if (user != null && user.Enabled)
                        return;
                }
                handleUnauthorizedRequest(filterContext);
            }
        }

        /// <summary>
        /// 处理结果，跳转登录界面
        /// </summary>
        /// <param name="filterContext"></param>
        private void handleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

    }
}
