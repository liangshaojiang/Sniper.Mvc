using Sniper.Admin.Framework.Controllers;
using Sniper.Admin.Framework.Security;
using Sniper.Admin.Models;
using Sniper.Core.Caching;
using Sniper.Core.Encyption;
using Sniper.Core.Librs;
using Sniper.Services.SysUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Controllers
{

    public class AccountController : BaseController
    {
        // GET: Account
        //登录随机数cookie名称
        private const string LOGIN_R = "l_r";

        private ISysUserRegistrationService _sysUserRegistrationService;
        private IAuthenticationService _authenticationService;
        private ISysUserService _sysUserService;
        private ICacheManager _cacheManager;

        public AccountController(ISysUserRegistrationService sysUserRegistrationService,
            IAuthenticationService authenticationService,
            ISysUserService sysUserService,
            ICacheManager cacheManager)
        {
            this._sysUserRegistrationService = sysUserRegistrationService;
            this._authenticationService = authenticationService;
            this._sysUserService = sysUserService;
            this._cacheManager = cacheManager;
        }

        [HttpGet]
        [Route("", Name = "login")]
        public ActionResult Login()
        {
            random();
            return View();
        }

        /// <summary>
        /// 设置随机数
        /// </summary>
        private void random()
        {
            string r = EncryptorHelper.GetMD5(Guid.NewGuid().ToString());
            //缓存
            _cacheManager.set(Session.SessionID, r, 60);
            CookieHelper.AddCookie(LOGIN_R, r, DateTime.Now.AddHours(1));
        }

        [Route("")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return Json(AjaxData, JsonRequestBehavior.DenyGet);
            if (!_cacheManager.isSet(Session.SessionID))
            {
                AjaxData.Message = "登陆超时，请重新试";
                random();
                return Json(AjaxData, JsonRequestBehavior.DenyGet);
            }
            EnumLoginResults result = _sysUserRegistrationService.ValidateUser(model.Account, model.Password, _cacheManager.get<string>(Session.SessionID));
            Entities.SysUser user = null;
            switch (result)
            {
                case EnumLoginResults.不存在:
                case EnumLoginResults.密码错误:
                    AjaxData.Message = "用户名或密码错误，请检查输入是否正确";
                    break;
                case EnumLoginResults.已被冻结:
                    AjaxData.Message = "此账号已被冻结，如有疑问请联系管理员";
                    break;
                case EnumLoginResults.已被锁定:
                    user = _sysUserService.getUserByAccount(model.Account);
                    TimeSpan? span = user.AllowLoginTime - DateTime.Now;
                    if (span.HasValue)
                    {
                        AjaxData.Message = "错误已达" + user.LoginFailedNum + "次，已被锁定：" + Math.Round(span.Value.TotalMinutes, 0) + "分钟";
                    }
                    break;
                case EnumLoginResults.成功:
                    AjaxData.Status = true;
                    AjaxData.Message = "登陆成功，正在进入系统...";
                    user = _sysUserService.getUserByAccount(model.Account);
                    _authenticationService.signIn(user, model.RememberMe);
                    break;
            }
            return Json(AjaxData, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 登录前获取用户的密码盐
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("salt", Name = "getSalt")]
        [OutputCache(Duration = 0)]
        public ActionResult Salt(string account)
        {
            var user = _sysUserService.getUserByAccount(account);
            if (user == null)
                return Content(EncryptorHelper.CreateSaltKey());
            return Content(user.Salt);
        }








    }
}