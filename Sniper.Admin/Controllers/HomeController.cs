using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sniper.Admin.Framework.Menu;
using Sniper.Admin.Framework.Controllers;

namespace Sniper.Admin.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : BaseAdminController
    {

        public HomeController()
        {

        }

        [Route("", Name = "homeIndex")]
        public ActionResult Index()
        { 
            return View();
        }

    }
}