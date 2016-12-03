using Sniper.Admin.Framework.Datatable;
using Sniper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Admin.Framework.Datatable
{
    /// <summary>
    /// 
    /// </summary>
    public static class IPageListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageList"></param>
        /// <param name="routeName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataSourceResult<T> toDataSourceResult<T>(this IPagedList<T> pageList, string routeName, dynamic param = null) where T : class  
        {
            return new DataSourceResult<T>(pageList, routeName, param);
        }
    }
}
