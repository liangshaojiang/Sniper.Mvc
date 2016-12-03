using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Sniper.Core.Infrastructure;

namespace Sniper.Core
{
    public abstract class BaseEngine : IEngine
    {
        protected ContainerManager _containerManager;

        /// <summary>
        /// 容器管理
        /// </summary>
        protected virtual ContainerManager ContainerManager
        {
            get
            {
                return this._containerManager;
            }
        }

        /// <summary>
        /// 注册依赖，需重写此方法
        /// </summary> 
        protected virtual void RegisterDependencies()
        {
            
        }

        /// <summary>
        /// 初始化
        /// </summary> 
        public void Initialize()
        {
            RegisterDependencies();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return _containerManager.Resolve<T>();
        }

        /// <summary>
        /// 批量创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
          return  _containerManager.ResolveAll<T>();
        }
        
    }
}
