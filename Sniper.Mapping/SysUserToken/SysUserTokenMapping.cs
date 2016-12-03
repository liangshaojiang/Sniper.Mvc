using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysUserToken
{
    [Serializable]
    public class SysUserTokenMapping
    {

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

    }
}
