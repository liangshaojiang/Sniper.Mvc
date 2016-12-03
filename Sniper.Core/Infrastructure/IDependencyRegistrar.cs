using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Infrastructure
{
    /// <summary>
    /// 反转注册
    /// </summary>
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder);
    }
}
