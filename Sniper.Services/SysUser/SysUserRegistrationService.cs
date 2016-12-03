using Sniper.Core.Encyption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Services.SysUser
{
    public class SysUserRegistrationService : ISysUserRegistrationService
    {
        private ISysUserService _sysUserService;

        public SysUserRegistrationService(ISysUserService sysUserService)
        {
            this._sysUserService = sysUserService;
        }

        /// <summary>
        /// 用户登录验证账号密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public EnumLoginResults ValidateUser(string account, string password,string r)
        {
            var user = _sysUserService.getUserByAccount(account);
            if (user == null)
                return EnumLoginResults.不存在;
            if (!user.Enabled)
                return EnumLoginResults.已被冻结;
            if (user.LoginLock)
                if (user.AllowLoginTime.HasValue && user.AllowLoginTime > DateTime.Now)
                    return EnumLoginResults.已被锁定;
            if (!EncryptorHelper.GetMD5(user.Password + r).Equals(password, StringComparison.InvariantCultureIgnoreCase))
            {
                user.LoginFailedNum++;
                switch (user.LoginFailedNum)
                {
                    case 3:
                        user.LoginLock = true;
                        //随机锁定5分钟以内的分钟数
                        user.AllowLoginTime = DateTime.Now.AddMinutes(new Random().Next(1,5));
                        break;
                    case 5:
                        user.LoginLock = true;
                        //随机锁定5-10分钟以内的分钟数
                        user.AllowLoginTime = DateTime.Now.AddMinutes(new Random().Next(10, 30));
                        break;
                    case 8:
                        user.LoginLock = true;
                        //随机锁定5-10分钟以内的分钟数
                        user.AllowLoginTime = DateTime.Now.AddMinutes(new Random().Next(50, 60));
                        break;
                    case 10:
                        user.LoginLock = true;
                        //随机锁定100-120分钟以内的分钟数
                        user.AllowLoginTime = DateTime.Now.AddMinutes(new Random().Next(100, 120));
                        break;
                    case 15:
                        user.LoginLock = true;
                        //锁定24个小时
                        user.AllowLoginTime = DateTime.Now.AddDays(1);
                        break;
                    default:
                        if (user.LoginFailedNum > 15)
                        {
                            user.LoginLock = true;
                            user.AllowLoginTime = DateTime.Now.AddYears(new Random().Next(10, 100));
                        }
                        break;
                }
                user.SysUserLoginLog.Add(new Entities.SysUserLoginLog()
                {
                    Id = Guid.NewGuid(),
                    IpAddress = Core.WebHelper.getClientIpAddress(),
                    LoginTime = DateTime.Now,
                    Message = "登入：失败"
                });
                _sysUserService.updateSysUser(user);
                return EnumLoginResults.密码错误;
            }
            else
            {
                user.LoginFailedNum = 0;
                user.AllowLoginTime = null;
                user.SysUserLoginLog.Add(new Entities.SysUserLoginLog()
                {
                    Id = Guid.NewGuid(),
                    IpAddress = Core.WebHelper.getClientIpAddress(),
                    LoginTime = DateTime.Now,
                    Message = "登入：成功"
                });
                user.LastIpAddress = Core.WebHelper.getClientIpAddress();
                user.LastLoginTime = DateTime.Now;
            }
            _sysUserService.updateSysUser(user);
            return EnumLoginResults.成功;
        }
    }
}
