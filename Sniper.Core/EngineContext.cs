 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Infrastructure;

namespace Sniper.Core
{
    public class EngineContext
    {
        private static IEngine _engine = null;

        /// <summary>
        /// 启动方法，线程类锁
        /// </summary>
        /// <param name="webApi"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(IEngine engine)
        {
            if (_engine == null)
            {
                _engine = engine;
                _engine.Initialize();
            }
            return _engine;
        }

        /// <summary>
        /// 替换引擎
        /// </summary>
        /// <param name="engine"></param>
        public static void Replace(IEngine engine)
        {
            _engine = engine;
        }

        /// <summary>
        /// 当前引擎
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (_engine == null)
                    throw new ArgumentException("引擎没有初始化");
                return _engine;
            }
        }
         
    }
}
