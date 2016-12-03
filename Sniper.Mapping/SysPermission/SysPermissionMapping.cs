using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysPermission
{
    [Serializable]
    public class PermissionRecordMapping
    {
        /// <summary>
		/// Id
        /// </summary>		
        public Guid Id { get; set; }

        /// <summary>
        /// 唯一资源标识符。格式：命名空间.控制器.方法名称
        /// </summary>		
        public string SysResource { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>		
        public Guid RoleId { get; set; }

        /// <summary>
        /// Creator
        /// </summary>		
        public Guid Creator { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>		
        public DateTime CreationTime { get; set; }
    }
}
