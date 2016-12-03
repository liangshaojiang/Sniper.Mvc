using Sniper.Admin.Framework.Infrastructure;
using Sniper.Admin.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Sniper.Admin.Framework.Controllers
{
    /// <summary>
    /// 登录后公用的基础控制器
    /// </summary>
    [AdminAuthorize]
    [DisabledSystem]
    public abstract class BasePublicController: BaseController
    {
        private IWorkContext _workContext = null;

        /// <summary>
        /// 当前工作上下文
        /// </summary>
        public IWorkContext WorkContext { get { return _workContext; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            _workContext = Core.EngineContext.Current.Resolve<IWorkContext>();
            if (_workContext.CurrentUser != null)
                System.Web.HttpContext.Current.Items["USERID"] = _workContext.CurrentUser.Id;
            base.Initialize(requestContext);
        }
    }
}
