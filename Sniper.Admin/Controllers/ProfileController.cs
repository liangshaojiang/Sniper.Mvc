using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Security;
using Sniper.Core;
using Sniper.Services.SysUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sniper.Core.Extensions;
using Sniper.Services.SysUserLoginLog;
using Sniper.Admin.Models;
using Sniper.Mapping.SysUser;
using Sniper.Mapping;

namespace Sniper.Admin.Controllers
{
    /// <summary>
    /// 个人中心
    /// </summary>
    [RoutePrefix("profile")]
    public class ProfileController : BasePublicController
    {
        // GET: Profile

        private ISysUserService _sysUserService;
        private ISysUserLoginLogService _sysUserLoginLogService;

        public ProfileController(ISysUserService sysUserService,
            ISysUserLoginLogService sysUserLoginLogService)
        {
            this._sysUserService = sysUserService;
            this._sysUserLoginLogService = sysUserLoginLogService;
        }

        /// <summary>
        /// 个人首页
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "profileIndex")]
        public ActionResult ProfileIndex()
        {
            ProfileModel model = new ProfileModel();
            model.User = WorkContext.CurrentUser;
            model.LoginLogList = _sysUserLoginLogService.profileLoginLog(model.User.Id);
            return View(model);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="avatar"></param>
        /// <returns></returns>
        [Route("avatarUpload", Name = "avatarUpload")]
        [HttpPost]
        public JsonResult AvatarUpload(HttpPostedFileBase avatar)
        {
            if (avatar == null)
                return Json(new { message="请选择头像图片" }, JsonRequestBehavior.DenyGet);
            if(!avatar.isImage())
                return Json(new { message = "不是图片文件" }, JsonRequestBehavior.DenyGet);
            if(avatar.isBigSize(103420))
                return Json(new { message = "图片大小超过100kb" }, JsonRequestBehavior.DenyGet);
            byte[] bt = new byte[avatar.ContentLength];
            avatar.InputStream.Read(bt, 0, bt.Length);
            _sysUserService.addAvatar(WorkContext.CurrentUser.Id, bt);
            return Json(new { status="OK",message="上传成功",url = Url.RouteUrl("avatar") }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 个人头像预览连接
        /// </summary>
        /// <returns></returns>
        [Route("avatar",Name = "avatar")]
        public void Avatar()
        {
            if (WorkContext.CurrentUser.Avatar == null)
                return;
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(WorkContext.CurrentUser.Avatar); 
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns></returns>
        [Route("changeInfo",Name = "changeInfo")]
        [HttpPost]
        public JsonResult ChangeInfo(SysUserMapping model)
        {
            if (!ModelState.IsValid)
            {
                AjaxData.Message = "数据验证未通过";
                return Json(AjaxData, JsonRequestBehavior.DenyGet);
            }
            model.Name = model.Name.Trim();
            model.Id = WorkContext.CurrentUser.Id;
            model.Modifier = WorkContext.CurrentUser.Id;
            _sysUserService.updateSysUser(model.toEntity());
            AjaxData.Message = "个人资料修改保存成功";
            AjaxData.Status = true;
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 个人修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("changePwd",Name ="changPassword")]
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                AjaxData.Message = "数据验证未通过";
                return Json(AjaxData, JsonRequestBehavior.DenyGet);
            }
            model.Password = model.Password.Trim();
            _sysUserService.changePassword(WorkContext.CurrentUser.Id, model.Password);
            AjaxData.Status = true;
            AjaxData.Message = "密码修改成功，下次登录请使用新密码。";
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        [Route("out", Name = "signOut")]
        public ActionResult SignOut()
        {
            EngineContext.Current.Resolve<IAuthenticationService>().signOut();
            return RedirectToRoute("login");
        }

    }
}