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
    public class DataSourceResult<T> where T : class 
    {
        public DataSourceResult(IPagedList<T> pageList, string routeName, dynamic param)
        {
            this.Data = pageList;
            this.Paging = new Pagination(pageList.PageIndex, pageList.PageSize, pageList.TotalCount, routeName, param);
            SearchArg = param;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IPagedList<T> Data { get; }

        /// <summary>
        /// 分页参数
        /// </summary>
        public Pagination Paging { get; }

        /// <summary>
        /// 
        /// </summary>
        public dynamic SearchArg { get; }
    }


}
