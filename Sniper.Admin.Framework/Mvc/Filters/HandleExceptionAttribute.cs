using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Framework.Mvc.Filters
{
    /// <summary>
    /// 错误处理特性
    /// </summary>
    public class HandleExceptionAttribute : HandleErrorAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.IsChildAction)
                return;
            if (filterContext.ExceptionHandled)
                return;
            Exception exception = filterContext.Exception;

            if (new HttpException(null, exception).GetHttpCode() != 500)
                return;
            if (!ExceptionType.IsInstanceOfType(exception))
                return;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            { 
                int code = new HttpException(null, filterContext.Exception).GetHttpCode();
                AjaxResult ajaxData = new AjaxResult();
                ajaxData.Code = code;
                ajaxData.Message = filterContext.Exception.Message;
                filterContext.Result = new JsonResult()
                {
                    Data = ajaxData,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
            }
            else
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }


    }
}
