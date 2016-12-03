using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sniper.Admin.Framework.Infrastructure;
using Sniper.Core;
using Sniper.Services.SysUser;

namespace Sniper.Admin.Framework.Mvc.Filters
{
    /// <summary>
    /// 最后多动时间过滤检测
    /// </summary>
    public class UserLastActivityAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 方法执行后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;

            if (filterContext.IsChildAction)
                return;

            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            var user = _workContext.CurrentUser;
            if(user.LastActivityTime==null || user.LastActivityTime.Value.AddMinutes(2.0)<DateTime.Now)
            {
                var _sysUserService = EngineContext.Current.Resolve<ISysUserService>();
                _sysUserService.lastActivityTime(user.Id);
            }
        }
         
    }
}
