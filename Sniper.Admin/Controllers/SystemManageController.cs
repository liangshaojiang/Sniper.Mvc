using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Menu;
using Sniper.Mapping.SysStore;
using Sniper.Services.SysStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Controllers
{
    [RoutePrefix("system")]
    [Descriper("系统管理", true, "menu-icon fa fa-desktop")]
    public class SystemManageController : BaseAdminController
    {
        // GET: SystemManager
        private ISysStoreService _sysStoreService;


        public SystemManageController(ISysStoreService sysStoreService)
        {
            this._sysStoreService = sysStoreService;
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "systemIndex")]
        [Descriper("系统设置", true, "menu-icon fa fa-cog ",FatherResource = "Sniper.Admin.Controllers.SystemManageController")]
        public ActionResult SystemIndex()
        {
            var model = _sysStoreService.getStore();
            return View(model);
        }

        /// <summary>
        /// 系统基础数据设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("editStore", Name = "editStore")]
        [Descriper("编辑系统基础设置", false, FatherResource = "Sniper.Admin.Controllers.SystemManageController.SystemIndex")]
        public JsonResult EditStore(SysStoreMapping model)
        {
            if(!ModelState.IsValid)
            {
                AjaxData.Message = "数据验证未通过";
                return Json(AjaxData, JsonRequestBehavior.DenyGet);
            }
            model.Creator = WorkContext.CurrentUser.Id;
            model.Modifier = WorkContext.CurrentUser.Id;
            _sysStoreService.saveStore(model);
            AjaxData.Status = true;
            AjaxData.Message = "管理系统设置成功";
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }

    }
}