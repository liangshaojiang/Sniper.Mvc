using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Services.SysUser
{
    /// <summary>
    /// 用户登录结果
    /// </summary>
    public enum EnumLoginResults
    {
        成功,

        密码错误,

        已被锁定,

        不存在,

        已被冻结
    }
}
