using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper.Admin.Models
{
    /// <summary>
    /// 登录对象
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage ="请输入账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="请输入密码")]
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }
    }
}