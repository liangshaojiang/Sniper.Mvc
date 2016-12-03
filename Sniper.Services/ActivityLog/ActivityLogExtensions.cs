using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core;
using Sniper.Services.SysLog;

namespace Sniper.Services.ActivityLog
{
    public static class ActivityLogExtensions
    {
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="entiy"></param>
        public static void entityInserted<T>(this IActivityLogService logService, T entiy) where T : class
        {
            insertActivityLog<T>(logService, "Inserted", entiy);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="entiy"></param>
        public static void entityUpdated<T>(this IActivityLogService logService, T entiy) where T : class
        {
            insertActivityLog<T>(logService, "Updated", entiy);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="entiy"></param>
        public static void entityDeleted<T>(this IActivityLogService logService, T entiy) where T : class
        {
            insertActivityLog<T>(logService, "Deleted", entiy);
        }

        /// <summary>
        /// 私有方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="method"></param>
        /// <param name="entity"></param>
        private static void insertActivityLog<T>(this IActivityLogService logService, string method, T entity) where T:class
        {
            try
            {
                var log = new Entities.ActivityLog()
                {
                    CreationTime = DateTime.Now,
                    Method = method,
                    Comment = Core.Librs.EntityToJson.EntityToJsonLog<T>(entity),
                    Id = Guid.NewGuid(),
                    ActivityName = typeof(T).Name
                };
                string _userId = System.Web.HttpContext.Current.Items["USERID"].ToString();
                if (_userId != null)
                {
                    Guid userId = Guid.Empty;
                    if (Guid.TryParse(_userId,out userId))
                    {
                        log.UserId = userId;
                        logService.InsertActivityLog(log);
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var sysLogService = EngineContext.Current.Resolve<ISysLogService>();
                    sysLogService.Error(ex.Message, ex);
                }
                catch (Exception)
                { 
                    //log4net

                }
            }
        }
    }
}
