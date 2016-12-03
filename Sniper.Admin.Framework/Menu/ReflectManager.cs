using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sniper.Core;
using Sniper.Core.Infrastructure;

namespace Sniper.Admin.Framework.Menu
{
    /// <summary>
    /// 菜单管理类
    /// </summary>
    public class ReflectManager
    {
        private readonly static ReflectManager _instance = new ReflectManager();

        /// <summary>
        /// 实例
        /// </summary>
        public static ReflectManager Instance { get { return _instance; } }

        /// <summary>
        /// 隐藏构造函数
        /// </summary>
        private ReflectManager()
        {

        }

        static ReflectManager()
        {
            _descripers = getDescriperAttribute();
        }

        private static List<DescriperAttribute> _descripers = null;

        /// <summary>
        /// 菜单方法描述
        /// </summary>
        public List<DescriperAttribute> Descripers
        {
            get
            {
                if (_descripers == null)
                    _descripers = getDescriperAttribute();
                return _descripers;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<DescriperAttribute> getDescriperAttribute()
        {
            List<DescriperAttribute> result = new List<DescriperAttribute>();
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            var asmList = typeFinder.GetAssemblies();
            List<Type> typeList = new List<Type>();
            foreach (var asm in asmList)
            {
                Type[] types = null;
                try
                {
                    types = asm.GetTypes();
                }
                catch
                {
                }
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        if (type.IsClass)
                        {
                            string s = type.FullName.ToLower();
                            if (s.EndsWith("controller"))
                                typeList.Add(type);
                        }
                    }
                }
            }
            foreach (var type in typeList)
            {
                //获取方法
                System.Reflection.MemberInfo[] members = type.FindMembers(System.Reflection.MemberTypes.Method,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly,
                Type.FilterName, "*");
                //反射控制器上的菜单描述
                object[] descriptionList = type.GetCustomAttributes(typeof(DescriperAttribute), false);
                DescriperAttribute fater = null;
                if (descriptionList != null && descriptionList.Length > 0)
                {
                    foreach (var dm in descriptionList)
                    {
                        fater = dm as DescriperAttribute;
                        if (String.IsNullOrEmpty(fater.SysResource))
                            fater.SysResource = type.FullName;
                        result.Add(fater);
                        break;
                    }
                }
                foreach (var m in members)
                {
                    if (m.DeclaringType.Attributes.HasFlag(System.Reflection.TypeAttributes.Public) != true)
                        continue;
                    //反射自定义属性DescriperAttribute,过滤不需要的  
                    object[] deserlist = m.GetCustomAttributes(typeof(DescriperAttribute), false);

                    if (deserlist != null && deserlist.Length > 0)
                    {
                        foreach (var cm in deserlist)
                        {
                            DescriperAttribute da = cm as DescriperAttribute;
                            if (String.IsNullOrEmpty(da.SysResource))
                                da.SysResource = type.FullName + "." + m.Name;
                            da.Controller = type.Name.Replace("Controller", "");
                            da.Action = m.Name;
                            //如果父级未指定
                            if (String.IsNullOrEmpty(da.FatherResource))
                                if (fater != null)
                                    da.FatherResource = fater.SysResource;
                            object[] routes = m.GetCustomAttributes(typeof(RouteAttribute), false);
                            if (routes != null && routes.Any())
                            {
                                var route = routes.First() as RouteAttribute;
                                da.RouteName = route.Name;
                            }
                            result.Add(da);
                            break;
                        }
                    }
                }
            }
            return result;
        }

    }
}
