using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core;
using Sniper.Core.Data;
using Sniper.Entities;
using Sniper.Mapping.SysLog;

namespace Sniper.Services.SysLog
{
    public class SysLogService : ISysLogService
    {
        private IRepository<Entities.SysLog> _sysLogRepository;


        public SysLogService(IRepository<Entities.SysLog> sysLogRepository)
        {
            this._sysLogRepository = sysLogRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearLog()
        {
            _sysLogRepository.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [SysLog]");
        }

        /// <summary>
        /// 插入系统运行日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="shortMessage"></param>
        /// <param name="fullMessage"></param>
        /// <returns></returns>
        public Entities.SysLog InsertLog(EnumLevel level, string shortMessage, string fullMessage = null)
        {
            var log = new Entities.SysLog()
            {
                Id = Guid.NewGuid(),
                FullMessage = fullMessage,
                ShortMessage = shortMessage,
                Level = (int)level,
                CreationTime = DateTime.Now,
                IpAddress = Core.WebHelper.getClientIpAddress(),
                PageUrl = Core.WebHelper.getUrl(),
                ReferrerUrl = Core.WebHelper.getUrlReferrer()
            };
            _sysLogRepository.insert(log);
            return log;
        }

        /// <summary>
        /// 管理查询
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IPagedList<Entities.SysLog> search(AdminSearchLogArg arg, int page, int size)
        {
            var query = _sysLogRepository.Table;
            if(arg!=null)
            {
                if (arg.level.HasValue)
                    query = query.Where(o => o.Level == arg.level);
            }
            query = query.OrderByDescending(o => o.CreationTime);
            return new PagedList<Entities.SysLog>(query, page, size);
        }
    }
}
