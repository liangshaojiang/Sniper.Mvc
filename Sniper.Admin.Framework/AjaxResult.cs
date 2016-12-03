using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Admin.Framework
{
    /// <summary>
    /// ajax操作返回的结果
    /// </summary>
    [Serializable]
    public class AjaxResult
    {
        /// <summary>
        /// 状态码 200 代表请求正常
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        
        /// <summary>
        /// 文字说明
        /// </summary>
        public string Message { get; set; }


    }
}
