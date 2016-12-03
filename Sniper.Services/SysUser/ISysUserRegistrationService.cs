using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Services.SysUser
{
    public interface ISysUserRegistrationService
    {
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="r">随机数</param>
        /// <returns></returns>
        EnumLoginResults ValidateUser(string account, string password,string r);


    }
}
