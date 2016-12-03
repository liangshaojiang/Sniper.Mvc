using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Menu;
using Sniper.Mapping.SysUser;
using Sniper.Services.SysUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sniper.Admin.Framework.Datatable;
using Sniper.Mapping;
using Sniper.Core.Encyption;
using Sniper.Core.Librs;

namespace Sniper.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("user")]
    public class UserController : BaseAdminController
    {
        // GET: User
        private ISysUserService _sysUserService;

        public UserController(ISysUserService sysUserService)
        {
            this._sysUserService = sysUserService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [Descriper("系统用户", true, "menu-icon glyphicon glyphicon-user", FatherResource = "Sniper.Admin.Controllers.SystemManageController")]
        [Route("", Name = "userIndex")]
        public ActionResult UserIndex(SysUserSearchArg arg, int page = 1, int size = 20)
        {
            var pageList = _sysUserService.searchUser(arg, page, size);
            var dataSource = pageList.toDataSourceResult<Entities.SysUser>("userIndex", arg);
            return View(dataSource);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("edit", Name = "editUser")]
        [Descriper("编辑系统用户", false, FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        public ActionResult EditUser(Guid? id, string returnUrl = null)
        {
            ViewBag.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.RouteUrl("userIndex");
            SysUserMapping model = new SysUserMapping();
            if (id.HasValue && id.Value != Guid.Empty)
            {
                var item = _sysUserService.getUserById(id.Value);
                if (item != null)
                    return View(item.toModel());
            }
            return View(model);
        }

        [HttpPost]
        [Route("edit")]
        public ActionResult EditUser(SysUserMapping model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.RouteUrl("userIndex");
            if (!ModelState.IsValid)
                return View(model);
            if (!String.IsNullOrEmpty(model.MobilePhone))
                model.MobilePhone = StringUitls.toDBC(model.MobilePhone);
            model.Name = model.Name.Trim();

            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                model.CreationTime = DateTime.Now;
                model.Salt = EncryptorHelper.CreateSaltKey();
                model.Account = StringUitls.toDBC(model.Account.Trim());
                model.Enabled = true;
                model.IsAdmin = false;
                model.Password = EncryptorHelper.GetMD5(model.Account + model.Salt);
                model.Creator = WorkContext.CurrentUser.Id;
                _sysUserService.insertSysUser(model.toEntity());
            }
            else
            {
                model.ModifiedTime = DateTime.Now;
                model.Modifier = WorkContext.CurrentUser.Id;
                _sysUserService.updateSysUser(model.toEntity());
            }
            return Redirect(ViewBag.ReturnUrl);
        }


        /// <summary>
        /// 设置启用与禁用账号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        [Route("enabled", Name = "enabled")]
        [Descriper("设置启用与禁用账号", false, FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        public JsonResult Enabled(Guid id, bool enabled)
        {
            _sysUserService.enabled(id, enabled, WorkContext.CurrentUser.Id);
            AjaxData.Message = "启用禁用设置完成";
            AjaxData.Status = true;
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 设置登录锁解锁与锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        [Route("loginLock", Name = "loginLock")]
        [Descriper("设置登录锁解锁与锁定", false, FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        public JsonResult LoginLock(Guid id, bool loginLock)
        {
            _sysUserService.loginLock(id, loginLock, WorkContext.CurrentUser.Id);
            AjaxData.Message = "登录锁状态设置完成";
            AjaxData.Status = true;
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}", Name = "deleteUser")]
        [Descriper("删除用户", false, FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        public JsonResult DeleteUser(Guid id)
        {
            _sysUserService.deleteUser(id, WorkContext.CurrentUser.Id);
            AjaxData.Status = true;
            AjaxData.Message = "删除完成";
            return Json(AjaxData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 远程验证账号是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("existAccount", Name = "remoteAccount")]
        [Descriper("远程验证账号是否存在", false, FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        public JsonResult RemoteAccount(string account)
        {
            account = account.Trim();
            return Json(!_sysUserService.existAccount(account), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("resetPwd/{id}",Name ="resetPassword")]
        [Descriper("重置用密码", false, FatherResource = "Sniper.Admin.Controllers.UserController.UserIndex")]
        public JsonResult ResetPassword(Guid id)
        {
            _sysUserService.resetPassword(id,WorkContext.CurrentUser.Id);
            AjaxData.Status = true;
            AjaxData.Message = "用户密码已重置为原始密码";
            return Json(AjaxData, JsonRequestBehavior.AllowGet);
        }
    }
}