using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysRole
{
    [Serializable]
    public class SysRoleMapping
    { 
        /// <summary>
        /// Id
        /// </summary>		
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>		
        [Required(ErrorMessage ="请输入角色的名称")]
        public string Name { get; set; }

        /// <summary>
        /// Creator
        /// </summary>		
        public Guid Creator { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>		
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Modifier
        /// </summary>		
        public Guid? Modifier { get; set; }

        /// <summary>
        /// ModifiedTime
        /// </summary>		
        public DateTime? ModifiedTime { get; set; }

        #region 扩展类型

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string SysUserName { get; set; }

        #endregion

    }
}
