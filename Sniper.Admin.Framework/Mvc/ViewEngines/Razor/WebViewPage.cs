using Sniper.Admin.Framework.Datatable;
using Sniper.Admin.Framework.Infrastructure;
using Sniper.Admin.Framework.Security;
using Sniper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Sniper.Admin.Framework.Mvc.Html;

namespace Sniper.Admin.Framework.Mvc.ViewEngines.Razor
{
    public abstract class WebViewPage : WebViewPage<dynamic>
    {

    }


    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private IWorkContext _workContext;

        /// <summary>
        /// 工作上下文
        /// </summary>
        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            this._workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="routeName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionRouteLink(string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            if (!authorize(routeName))
                return null;
            return Html.RouteLink(linkText, routeName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="routeName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionRouteLink(string linkText, string routeName, object routeValues)
        {
            if (!authorize(routeName))
                return null;
            return Html.RouteLink(linkText, routeName, routeValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionRouteLink(string linkText, string routeName)
        {
            if (!authorize(routeName))
                return null;
            return Html.RouteLink(linkText, routeName);
        }

        /// <summary>
        /// 包含图标的连接按钮
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="routeName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="iconHtmlAttributes"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionRouteLinkIcon(string linkText, string routeName, RouteValueDictionary routeValues, object htmlAttributes, object iconHtmlAttributes)
        { 
            if (!authorize(routeName))
                return null;
           return Html.RouteLinkIcon(linkText, routeName, routeValues, htmlAttributes, iconHtmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionActionLink(string linkText, string actionName)
        {
            string controller = (string)Request.RequestContext.RouteData.Values["controller"];
            if (!authorize(actionName, controller))
                return null;
            return Html.ActionLink(linkText, actionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionActionLink(string linkText, string actionName, string controllerName)
        {
            if (!authorize(actionName, controllerName))
                return null;
            return Html.ActionLink(linkText, actionName, controllerName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionActionLink(string linkText, string actionName, string controllerName, object routeValues)
        {
            if (!authorize(actionName, controllerName))
                return null;
            return Html.ActionLink(linkText, actionName, controllerName, routeValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public MvcHtmlString PermissionActionLink(string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            if (!authorize(actionName, controllerName))
                return null;
            return Html.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="sysResource"></param>
        /// <returns></returns>
        public bool authorizeSysResource(string sysResource)
        {
            var _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            if (_workContext.CurrentUser == null)
                return false;
            return _authenticationService.authorize(sysResource);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool authorize(string routeName)
        {
            var route = (System.Web.Routing.Route)RouteTable.Routes[routeName];
            string action = (string)route.Defaults["action"];
            string controller = (string)route.Defaults["controller"];  
            return authorize(action, controller);
        }

        private bool authorize(string action, string controller)
        {
            var _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            if (_workContext.CurrentUser == null)
                return false;
            return _authenticationService.authorize(action, controller);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MvcHtmlString Pagination(Pagination model)
        {
            return Html.Partial("_Pagination",model);
        }

        /// <summary>
        /// 必填*号提示
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString RequiredSpan()
        {
           return Html.RequiredSpan();
        }
    }



}
