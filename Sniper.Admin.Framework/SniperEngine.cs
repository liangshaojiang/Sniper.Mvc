using Autofac;
using Autofac.Integration.Mvc;
using Sniper.Core;
using Sniper.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sniper.Admin.Framework
{
    public class SniperEngine:BaseEngine,IEngine
    { 
        /// <summary>
        /// 
        /// </summary>
        protected override void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            //放入容器管理类
            //
            var typeFinder = new AppDomainTypeFinder();
            builder = new ContainerBuilder();
            //将当前引擎注册
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            new DependencyRegistrar().Register(builder);
            //构建容器
            var container = builder.Build();
            this._containerManager = new ContainerManager(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
