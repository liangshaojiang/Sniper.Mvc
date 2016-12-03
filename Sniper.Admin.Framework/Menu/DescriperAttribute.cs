using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Admin.Framework.Menu
{
    /// <summary>
    /// 菜单描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class DescriperAttribute : Attribute
    {
        public DescriperAttribute()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isMenu"></param>
        public DescriperAttribute(string name,bool isMenu)
        {
            this.Name = name;
            this.IsMenu = isMenu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cssClass"></param>
        public DescriperAttribute(string name, string cssClass)
        {
            this.Name = name;
            this.CssClass = cssClass;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isMenu"></param>
        /// <param name="cssClass"></param>
        public DescriperAttribute(string name, bool isMenu, string cssClass)
        {
            this.Name = name;
            this.IsMenu = isMenu; 
            this.CssClass = cssClass;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 统一资源定位标识符
        /// 格式：namespace.Controller.Action，或 namespace.Controller
        /// </summary>
        public string SysResource { get; set; }

        /// <summary>
        /// 上级统一资源定位标识。
        /// SysResource 的值
        /// </summary>
        public string FatherResource { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// css样式
        /// </summary>
        public string CssClass { get; set; }


    }
}
