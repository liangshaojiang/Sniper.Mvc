using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysUserRole
{
    [Serializable]
    public class SysUserRoleMapping
    {
        /// <summary>
        /// Id
        /// </summary>		
        public Guid Id { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>		
        public Guid RoleId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>		
        public Guid UserId { get; set; }

    }
}
