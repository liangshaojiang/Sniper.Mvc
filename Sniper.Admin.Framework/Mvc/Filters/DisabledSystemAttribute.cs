using Sniper.Admin.Framework.Infrastructure;
using Sniper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sniper.Admin.Framework.Mvc.Filters
{
    public class DisabledSystemAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 在方法执行之前检测，系统是否暂停使用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext 为空");

            if (filterContext.IsChildAction)
                return;

            var _workContext = EngineContext.Current.Resolve<IWorkContext>();

            if(_workContext.Store!=null && _workContext.Store.Disabled)
            {
                if (_workContext.CurrentUser.IsAdmin)
                    return;
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new AjaxResult() { Status = false, Message = "系统暂停使用" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult() { ViewName = "DisabledSystem" };
                }
            }

        }
    }
}
