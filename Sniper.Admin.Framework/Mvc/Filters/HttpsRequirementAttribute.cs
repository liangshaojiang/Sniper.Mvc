using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sniper.Core;
using Sniper.Admin.Framework.Infrastructure;

namespace Sniper.Admin.Framework.Mvc.Filters
{
    /// <summary>
    /// https 请求过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext为空");
            //子请求
            if (filterContext.IsChildAction)
                return;

            // 只对GET请求从定向
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;
            //是不是https请求
            if (!WebHelper.isSecureConnection())
            {
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();
                if (_workContext.Store != null && _workContext.Store.SslEnabled)
                {
                    string url = WebHelper.getThisPageUrl(true, true);
                    //301 跳转
                    filterContext.Result = new RedirectResult(url, true);
                }
            }
        }
    }
}
