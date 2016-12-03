using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Menu;
using Sniper.Services.SysRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sniper.Mapping;
using Sniper.Mapping.SysRole;
using Sniper.Services.SysPermission;

namespace Sniper.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("role")]
    public class RoleController : BaseAdminController
    {
        // GET: Role
        private ISysRoleService _sysRoleService;
        private ISysPermissionService _sysPermissionService;

        public RoleController(ISysRoleService sysRoleService, ISysPermissionService sysPermissionService)
        {
            this._sysRoleService = sysRoleService;
            this._sysPermissionService = sysPermissionService;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [Descriper("角色列表", true, "menu-icon fa fa-cogs", FatherResource = "Sniper.Admin.Controllers.SystemManageController")]
        [Route("", Name = "roleIndex")]
        public ActionResult RoleIndex(AdminSearchRoleArg arg)
        {
            return View(_sysRoleService.searchRoles(arg));
        }

        /// <summary>
        /// 新增修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("edit", Name = "editRole")]
        [Descriper("新增修改角色", false, FatherResource = "Sniper.Admin.Controllers.RoleController.RoleIndex")]
        [HttpGet]
        public ActionResult EditRole(Guid? id)
        {
            SysRoleMapping model = new SysRoleMapping();
            if (id.HasValue && id.Value != Guid.Empty)
            {
                var item = _sysRoleService.getRole(id.Value);
                if (item != null)
                    model = item.toModel();
            }
            return View(model);
        }
        [Route("edit")]
        [HttpPost]
        public ActionResult EditRole(SysRoleMapping model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Name = model.Name.Trim();
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                model.CreationTime = DateTime.Now;
                model.Creator = WorkContext.CurrentUser.Id;
                _sysRoleService.InserRole(model);
            }
            else
            {
                model.ModifiedTime = DateTime.Now;
                model.Modifier = WorkContext.CurrentUser.Id;
                _sysRoleService.UpdateRole(model);
            }
            return RedirectToRoute("roleIndex");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}", Name = "deleteRole")]
        [Descriper("删除角色", false, FatherResource = "Sniper.Admin.Controllers.RoleController.RoleIndex")]
        public JsonResult DeleteRole(Guid id)
        {
            _sysRoleService.DeleteRole(id);
            AjaxData.Status = true;
            AjaxData.Message = "角色已删除成功";
            return Json(AjaxData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色权限设置
        /// </summary>
        /// <returns></returns>
        [Route("permission", Name = "rolePermission")]
        [Descriper("角色权限设置", false, FatherResource = "Sniper.Admin.Controllers.RoleController.RoleIndex")]
        [HttpGet]
        public ActionResult RolePermission(Guid id)
        {
            RolePermissionViewModel model = new RolePermissionViewModel();
            var roleList = _sysRoleService.getAllRoles();
            if(roleList ==null || !roleList.Any())
                return RedirectToRoute("roleIndex");
            model.Role = roleList.FirstOrDefault(o=>o.Id==id);
            if (model.Role == null)
                return RedirectToRoute("roleIndex");
            model.RoleList = roleList;
            model.Permissions = _sysPermissionService.getByRoleId(id);
            return View(model);
        }
         
        [HttpPost]
        [Route("permission")]
        public JsonResult RolePermission(Guid id,List<string> sysResource)
        {
            _sysPermissionService.SavePermissionRecord(id, sysResource, WorkContext.CurrentUser.Id);
            AjaxData.Status = true;
            AjaxData.Message = "角色权限设置成功";
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }
    }
}