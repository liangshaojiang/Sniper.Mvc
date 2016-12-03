using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sniper.Admin.Framework.Datatable
{
    public class Pagination
    {


        public Pagination(int pageIndex, int pageSize, int totalCount, string routeName, object param = null)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPages = TotalCount / pageSize;
            if (TotalCount % pageSize > 0)
                this.TotalPages++;
            string linkUrl = "";
            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            if (!String.IsNullOrEmpty(routeName))
                linkUrl = Url.RouteUrl(routeName, param);
            linkUrl = linkUrl.Any(o => o == '?') ? linkUrl + "&page={0}&size={1}" : linkUrl + "?page={0}&size={1}";
            this.LinkUrl = linkUrl;
            this.CurrentUrl = String.Format(linkUrl, pageIndex,pageSize);
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 每页数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总记录
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// 分页数
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        /// <summary>
        /// 分页链接地址
        /// </summary>
        public string LinkUrl { get;  }

        /// <summary>
        /// 当前页的链接
        /// </summary>
        public string CurrentUrl { get; }
    }
}
