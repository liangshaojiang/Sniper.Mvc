using Sniper.Mapping.SysUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sniper.Admin.Models
{
    /// <summary>
    /// 个人中心对象
    /// </summary>
    public class ProfileModel
    {
        public ProfileModel()
        {
            ChangePassword = new ChangePasswordModel();
        }

        /// <summary>
        /// 用户
        /// </summary>
        public SysUserMapping User { get; set; }

        /// <summary>
        /// 登录日记
        /// </summary>
        public IEnumerable<Entities.SysUserLoginLog> LoginLogList { get; set; }

        /// <summary>
        /// 修改密码对象
        /// </summary>
        public ChangePasswordModel ChangePassword { get; set; }
    }
}