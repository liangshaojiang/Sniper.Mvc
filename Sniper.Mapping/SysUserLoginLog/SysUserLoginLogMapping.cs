using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysUserLoginLog
{
    /// <summary>
    /// SysUserLoginLog
    /// </summary>
    [Serializable]
    public class SysUserLoginLogMapping
    {
        /// <summary>
        /// Id 
        /// </summary>		 
        public Guid Id { get; set; }

        /// <summary>
        /// UserId 
        /// </summary>		 
        public Guid UserId { get; set; }

        /// <summary>
        /// IpAddress 
        /// </summary>		 
        public string IpAddress { get; set; }

        /// <summary>
        /// LoginTime 
        /// </summary>		 
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// Message 
        /// </summary>		 
        public string Message { get; set; }

    }
}
