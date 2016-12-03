using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sniper.Admin.Framework.Mvc.Filters
{
    /// <summary>
    /// POST请求CSRF过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PublicAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
               
            if (filterContext.IsChildAction)
                return;

            //只过滤POST请求
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                return;
             
            var validator = new ValidateAntiForgeryTokenAttribute();
            validator.OnAuthorization(filterContext);
        }
    }
}
