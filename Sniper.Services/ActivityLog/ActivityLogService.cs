using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Data;
using Sniper.Entities;

namespace Sniper.Services.ActivityLog
{
    public class ActivityLogService : IActivityLogService
    {
        private IRepository<Entities.ActivityLog> _activityLogRepository;

        public ActivityLogService(IRepository<Entities.ActivityLog> activityLogRepository)
        {
            this._activityLogRepository = activityLogRepository;
        }

        /// <summary>
        /// 插入实体更新日志
        /// </summary>
        /// <param name="log"></param>
        public void InsertActivityLog(Entities.ActivityLog log)
        {
            _activityLogRepository.insert(log);
        }
         

    }
}
