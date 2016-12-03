using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Sniper.Core.Infrastructure;
using System.Data.Entity;
using Sniper.Data;
using Sniper.Core.Data;
using Sniper.Core.Caching;
using Autofac.Integration.Mvc;
using System.Reflection;
using Sniper.Admin.Framework.Security;

namespace Sniper.Admin.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Register(ContainerBuilder builder)
        { 
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.Load("Sniper.Admin"));
            builder.Register<DbContext>(x=>new Entities.SniperDbcontext()).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            //
            var assembly = Assembly.Load("Sniper.Services");
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        }
    }
}
