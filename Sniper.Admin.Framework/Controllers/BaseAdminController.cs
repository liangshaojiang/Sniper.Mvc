using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Sniper.Admin.Framework.Mvc.Filters;
using Sniper.Admin.Framework.Infrastructure;

namespace Sniper.Admin.Framework.Controllers
{
   [PublicAntiForgery]
   [UserLastActivity]
   [Permission]
   public abstract class BaseAdminController: BasePublicController
    {
         
    }
}
