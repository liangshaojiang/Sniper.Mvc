using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Sniper.Core.Infrastructure
{
    public class AppDomainTypeFinder : ITypeFinder
    {
        private string assemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
        private string assemblyRestrictToLoadingPattern = ".*";


        /// <summary>
        /// 查找类型，不支持泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<Type> FindClassesOfType<T>()
        {
            var result = new List<Type>();
            var assemblies = GetAssemblies();
            foreach (var a in assemblies)
            {
                Type[] types = null;
                try
                {
                    types = a.GetTypes();
                }
                catch(Exception)
                {
                    types = null;
                }
                if(types!=null)
                {
                    var assignTypeFrom = typeof(T);
                    foreach (var t in types)
                    {
                        if(assignTypeFrom.IsAssignableFrom(t))
                        {
                            if(!t.IsInterface)
                            {
                                if (t.IsClass && !t.IsAbstract)
                                    result.Add(t);
                            }
                        }
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns></returns>
        public IList<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Matches(assembly.FullName))
                {
                    assemblies.Add(assembly);
                }
            }
           return assemblies;
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="assemblyFullName"></param>
        /// <returns></returns>
        public virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, assemblySkipLoadingPattern)
                   && Matches(assemblyFullName, assemblyRestrictToLoadingPattern);
        }
        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="assemblyFullName"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }


    }
}
