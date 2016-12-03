using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sniper.Core;
using Sniper.Services.SysLog;
using System.Web;
using Sniper.Admin.Framework.Mvc.Filters;

namespace Sniper.Admin.Framework.Controllers
{
   [HttpsRequirement]
    public class BaseController : Controller
    {
        /// <summary>
        /// ajax 操作请求返回的结果
        /// </summary>
        public AjaxResult AjaxData { get; }

        public BaseController()
        {
            this.AjaxData = new AjaxResult();
        }

        /// <summary>
        /// 系统错误记录日志
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
                logException(filterContext.Exception);
            base.OnException(filterContext);
        }

        /// <summary>
        /// 错误日志记录
        /// </summary>
        /// <param name="exc"></param>
        protected void logException(Exception exc)
        {
            try
            {
                var logger = EngineContext.Current.Resolve<ISysLogService>();
                logger.Error(exc.Message, exc);
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("_sys_e").Error(e);
            }

        }
    }
}
