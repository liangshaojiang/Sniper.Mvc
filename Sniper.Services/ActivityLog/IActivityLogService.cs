using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Services.ActivityLog
{
    public interface IActivityLogService
    {
        /// <summary>
        /// 插入实体类操作日志
        /// </summary>
        /// <param name="logi"></param>
        void InsertActivityLog(Entities.ActivityLog log);
         


    }
}
