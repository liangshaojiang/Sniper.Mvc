using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sniper.Core;
using Sniper.Services.SysLog;
using Sniper.Admin.Framework;

namespace Sniper.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            //
            EngineContext.Initialize(new SniperEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            try
            {
                var logger = EngineContext.Current.Resolve<ISysLogService>();
                logger.Information("Admin 系统启动", null);
            }
            catch (Exception)
            {

            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }
        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {


        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            logException(exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exc"></param>
        protected void logException(Exception exc)
        {
            if (exc == null)
                return;

            var httpException = exc as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
                return;

            try
            {
                var logger = EngineContext.Current.Resolve<ISysLogService>();
                logger.Error(exc.Message, exc);
            }
            catch (Exception)
            {
                log4net.LogManager.GetLogger("global").Error(exc);
            }
        }
    }
}
