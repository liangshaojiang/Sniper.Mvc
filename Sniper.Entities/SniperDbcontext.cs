using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Entities
{
    public class SniperDbcontext : SniperEntities
    {
        public SniperDbcontext():base()
        {

        }

        public SniperDbcontext(string connectionStringName) :base(connectionStringName)
        {

        }

    }
}
