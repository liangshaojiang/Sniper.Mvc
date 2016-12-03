using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sniper.Admin.Framework.Mvc.Html
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 必填项目信号提醒
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString RequiredSpan(this HtmlHelper htmlHelper)
        {
            var tagBuilder = new TagBuilder("span");
            tagBuilder.AddCssClass("required-span");
            tagBuilder.SetInnerText("*");
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// 扩展a连接，带图标的连接图标i标签
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">链接文字</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">a属性</param>
        /// <param name="iconHtmlAttributes">图标属性</param>
        /// <returns></returns>
        public static MvcHtmlString RouteLinkIcon(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, object htmlAttributes, object iconHtmlAttributes)
        {
            TagBuilder iconBuilder = new TagBuilder("i");
            iconBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
            string url = UrlHelper.GenerateUrl(routeName, null, null, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, false);
            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = iconBuilder.ToString(TagRenderMode.Normal) + ((!String.IsNullOrEmpty(linkText)) ? HttpUtility.HtmlEncode(linkText) : String.Empty)
            };
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("href", url);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}
