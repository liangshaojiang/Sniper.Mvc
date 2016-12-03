using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sniper.Core.Infrastructure
{
    public class ContainerManager
    {
        //只读的IContainer 容器
        private readonly IContainer _container;

        public ContainerManager(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// 容器对象
        /// </summary>
        public virtual IContainer Container
        {
            get
            {
                return _container;
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
                scope = Scope();
            if (String.IsNullOrEmpty(key))
                return scope.Resolve<T>();
            return scope.ResolveKeyed<T>(key);
        }

        /// <summary>
        /// 批量创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (scope == null)
                scope = Scope();
            if (string.IsNullOrEmpty(key))
                return scope.Resolve<IEnumerable<T>>().ToArray();
            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        /// <summary>
        /// ILifetimeScope
        /// </summary>
        /// <returns></returns>
        public virtual ILifetimeScope Scope()
        {
            try
            { 
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }

        }
    }
}
