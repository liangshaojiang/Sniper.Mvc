using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Admin.Framework.Security
{
    /// <summary>
    /// 登录结果回馈
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 验证状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Message { get; set; }
    }
}
