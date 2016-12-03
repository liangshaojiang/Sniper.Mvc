using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Services.SysUserLoginLog
{
    public interface ISysUserLoginLogService
    {
        /// <summary>
        /// 个人中心最近登录记录，默认8条
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        IEnumerable<Entities.SysUserLoginLog> profileLoginLog(Guid userId,int size = 5);


    }
}
