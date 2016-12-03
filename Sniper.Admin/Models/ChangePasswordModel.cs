using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper.Admin.Models
{
    public class ChangePasswordModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="请输入密码")]
        [StringLength(512,MinimumLength =6,ErrorMessage ="密码不少于6位字符")]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="请输入确认密码")]
        [Compare("Password", ErrorMessage ="确认密码和密码不一样")]
        public string ConfirmPassword { get; set; }


    }
}