using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sniper.Mapping.SysUser
{
    [Serializable]
    public class SysUserMapping
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="请输入账号")]
        [RegularExpression(@"^\w{5,18}$",ErrorMessage ="5~18位字母数字及下划线")]
        [Remote("remoteAccount", ErrorMessage ="账号已经存在")]
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="请输入用户名称")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.EmailAddress,ErrorMessage ="邮箱格式错误")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",ErrorMessage = "邮箱格式错误")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.PhoneNumber,ErrorMessage ="手机号码错误")]
        [RegularExpression(@"^1[345678]\d{9}$",ErrorMessage = "手机号码格式错误")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LoginFailedNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AllowLoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LoginLock { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastActivityTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeletedTime { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? Modifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? Creator { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}
