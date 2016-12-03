using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping.SysLog
{
    public enum EnumLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 10,

        /// <summary>
        /// 一般
        /// </summary>
        Information = 20,

        /// <summary>
        /// 警告
        /// </summary>
        Warning = 30,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 40,

        /// <summary>
        /// 致命
        /// </summary>
        Fatal = 50
    }
}
