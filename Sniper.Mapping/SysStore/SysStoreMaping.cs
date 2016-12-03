using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysStore
{
    /// <summary>
 	/// 系统描述表，一条数据
 	/// </summary>
	[Serializable]
    public class SysStoreMapping
    {
        /// <summary>
        /// Id 
        /// </summary>		 
        public Guid Id { get; set; }

        /// <summary>
        /// 名称系统 
        /// </summary>		 
        [Required(ErrorMessage ="请输入显示名称")]
        public string Name { get; set; }

        /// <summary>
        /// 启用Ssl 
        /// </summary>		 
        public bool SslEnabled { get; set; }

        /// <summary>
        /// 系统停用 
        /// </summary>		 
        public bool Disabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="请输入图标样式")]
        public string IconClass { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName { get; set; }

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

    }
}
