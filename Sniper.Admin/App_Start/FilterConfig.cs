using Sniper.Admin.Framework.Mvc.Filters;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 统一过滤器注册
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           filters.Add(new HandleExceptionAttribute());
        }
    }
}
