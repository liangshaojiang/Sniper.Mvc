using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Datatable;
using Sniper.Services.SysRole;
using Sniper.Services.SysUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Controllers
{
    [RoutePrefix("widget")]
    public class WidgetController : BasePublicController
    {
        // GET: Widget
        private ISysUserService _sysUserService;
        private ISysRoleService _sysRoleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysUserService"></param>
        public WidgetController(ISysUserService sysUserService,
            ISysRoleService sysRoleService)
        {
            this._sysUserService = sysUserService;
            this._sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 系统用户下拉选择框
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult ChosenUser(Guid? id)
        {
            if (id != null && id.Value != Guid.Empty)
            {
                var item = _sysUserService.getUserById(id.Value);
                return PartialView(item);
            }
            return PartialView();
        }

        /// <summary>
        /// select2下拉操作员
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [Route("selset2/users", Name = "userData")]
        public JsonResult UserData(string q)
        {
            var list = _sysUserService.searchUser(new Mapping.SysUser.SysUserSearchArg() { q = q }, 1, 20);
            if (list != null && list.Any())
            {
                var result = list.Select(item => new Select2Model { id = item.Id.ToString(), text = item.Name }).ToList();
                result.Insert(0, new Select2Model { id = "", text = "选择操作员" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult SelectRole(Guid ? id)
        {
            ViewBag.RoleId = id;
            return PartialView(_sysRoleService.getAllRoles());
        }




    }
}