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
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        private readonly bool _dontValidate;

        public PermissionAttribute() : this(false)
        {

        }

        public PermissionAttribute(bool dontValidate)
        {
            this._dontValidate = dontValidate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        private IEnumerable<PermissionAttribute> getPermissionAttributes(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof(PermissionAttribute), true)
               .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(PermissionAttribute), true))
               .OfType<PermissionAttribute>();

        }

        /// <summary>
        /// 这是需要权限的页面
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool isPermissionPageRequested(ActionExecutingContext filterContext)
        {
            var adminAttributes = getPermissionAttributes(filterContext.ActionDescriptor);
            if (adminAttributes != null && adminAttributes.Any())
                return true;
            return false;
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
        /// 方法执行前进行权限判断
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext 为空");

            if (_dontValidate)
                return;
            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
                throw new InvalidOperationException("子方法不能添加权限验证特性");
            //允许匿名访问
            if (getAllowAttributes(filterContext.ActionDescriptor))
                return;
            if (isPermissionPageRequested(filterContext))
            {
                var _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
                if (!_authenticationService.authorize(filterContext))
                    handleRequest(filterContext);
            }
        }

        /// <summary>
        /// 处理结果，跳转登录界面
        /// </summary>
        /// <param name="filterContext"></param>
        private void handleRequest(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new AjaxResult() { Status = false, Message = "您没有操作权限" },
                    JsonRequestBehavior =JsonRequestBehavior.AllowGet,
                };
            }
            else
            {
                filterContext.Result = new ViewResult() { ViewName = "NotPermission" };
            }
        }

    }
}
