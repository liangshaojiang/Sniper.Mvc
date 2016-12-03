using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Entities
{
    public partial class SniperEntities
    {
        /// <summary>
        /// 带参的构造函数
        /// </summary>
        /// <param name="connectionStringName"></param>
        public SniperEntities(string connectionStringName)
            :base(connectionStringName)
        {

        }

    }
}
