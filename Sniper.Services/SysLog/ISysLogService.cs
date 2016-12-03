using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysLog;
using Sniper.Core;

namespace Sniper.Services.SysLog
{
    public interface ISysLogService
    {  
        /// <summary>
        /// 清除日志
        /// </summary>
        void ClearLog();

        /// <summary>
        /// 插入系统日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="shortMessage"></param>
        /// <param name="fullMessage"></param>
        /// <returns></returns>
        Entities.SysLog InsertLog(EnumLevel level, string shortMessage, string fullMessage = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        IPagedList<Entities.SysLog> search(AdminSearchLogArg arg, int page, int size);
    }
}
