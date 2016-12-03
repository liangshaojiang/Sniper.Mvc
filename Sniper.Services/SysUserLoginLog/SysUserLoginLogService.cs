using Sniper.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Entities;

namespace Sniper.Services.SysUserLoginLog
{
    public class SysUserLoginLogService: ISysUserLoginLogService
    {
        private IRepository<Entities.SysUserLoginLog> _sysUserLoginLogRepository;

        public SysUserLoginLogService(IRepository<Entities.SysUserLoginLog> sysUserLoginLogRepository)
        {
            this._sysUserLoginLogRepository = sysUserLoginLogRepository;
        }

        /// <summary>
        /// 个人中心最近登录记录，默认5条
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Entities.SysUserLoginLog> profileLoginLog(Guid userId, int size = 5)
        {
           return _sysUserLoginLogRepository.Table.Where(o => o.UserId == userId).OrderByDescending(o => o.LoginTime).Take(size).ToList();
        }
    }
}
