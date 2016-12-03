using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Datatable;
using Sniper.Admin.Framework.Menu;
using Sniper.Mapping.SysLog;
using Sniper.Services.SysLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Controllers
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [RoutePrefix("syslog")]
    public class SysLogController : BaseAdminController
    {
        // GET: SysLog
        private ISysLogService _sysLogService;

        public SysLogController(ISysLogService sysLogService)
        {
            this._sysLogService = sysLogService;
        }
        [Route("",Name = "sysLogIndex")]
        [Descriper("系统日志", true, "menu-icon fa fa-inbox", FatherResource = "Sniper.Admin.Controllers.SystemManageController")]
        public ActionResult SysLogIndex(AdminSearchLogArg arg,int page = 1,int size = 20)
        {
            var pageList = _sysLogService.search(arg, page, size);
            var dataSource = pageList.toDataSourceResult<Entities.SysLog>("sysLogIndex", arg);
            return View(dataSource);
        }
    }
}