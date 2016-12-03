using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Menu;
using Sniper.Mapping.SysUserRole;
using Sniper.Services.SysRole;
using Sniper.Services.SysUser;
using Sniper.Services.SysUserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Controllers
{
    /// <summary>
    /// 用户角色设置控制器
    /// </summary>
    [RoutePrefix("userRole")]
    public class UserRoleController : BaseAdminController
    {
        // GET: UserRole
        private ISysUserRoleService _sysUserRoleService;
        private ISysUserService _sysUserService;
        private ISysRoleService _sysRoleService;

        public UserRoleController(ISysUserRoleService sysUserRoleService,
            ISysUserService sysUserService,
            ISysRoleService sysRoleService)
        {
            this._sysUserRoleService = sysUserRoleService;
            this._sysUserService = sysUserService;
            this._sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <returns></returns>
        [Route("edit", Name = "editUserRole")]
        [Descriper("设置用户角色", false, "menu-icon glyphicon glyphicon-user", FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        [HttpGet]
        public ActionResult EditUserRole(Guid id, string returnUrl)
        {
            UserRoleViewModel model = new UserRoleViewModel();
            ViewBag.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.RouteUrl("userIndex");
            model.SysUser = _sysUserService.getUserById(id);
            if (model.SysUser == null)
                return Redirect(ViewBag.ReturnUrl);
            model.UserRoleList = _sysUserRoleService.getRoleByUserId(id);
            model.RoleList = _sysRoleService.getAllRoles();
            if(model.RoleList==null || !model.RoleList.Any())
                return RedirectToRoute("roleIndex");
            return View(model);
        }

        [Route("edit")]
        [HttpPost]
        public ActionResult EditUserRole(Guid id,List<Guid> roleIds,string returnUrl)
        {
            _sysUserRoleService.SaveUserRole(id, roleIds);
            returnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.RouteUrl("userIndex");
            return Redirect(returnUrl);
        }
    }
}