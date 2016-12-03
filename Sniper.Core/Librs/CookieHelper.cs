using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sniper.Core.Librs
{
    public class CookieHelper
    {
        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void Add(HttpCookie cookie)
        { 
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        public static void AddCookie(string name, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加只读cookie，不设置过期时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="httpOnly">只读</param>
        public static void AddCookie(string name, string value, bool httpOnly)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.HttpOnly = httpOnly;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加只读cookie和设置过期时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        /// <param name="httpOnly">只读</param>
        public static void AddCookie(string name, string value, DateTime expires, bool httpOnly)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = expires;
            cookie.HttpOnly = httpOnly;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        /// <param name="domain">域</param>
        /// <param name="httpOnly">只读</param>
        public static void AddCookie(string name, string value, DateTime expires, string domain, bool httpOnly)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = expires;
            cookie.HttpOnly = httpOnly;
            cookie.Domain = domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="domain">域</param>
        /// <param name="httpOnly">只读</param>
        public static void AddCookie(string name, string value, string domain, bool httpOnly)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.HttpOnly = httpOnly;
            cookie.Domain = domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="domain"></param>
        public static void AddCookie(string name, string value, string domain)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Domain = domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <param name="domain"></param>
        public static void AddCookie(string name, string value, DateTime expires, string domain)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Domain = domain;
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="domain"></param>
        /// <param name="expires"></param>
        public static void AddCookie(string name, string value, string domain, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Domain = domain;
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="domain"></param>
        /// <param name="expires"></param>
        /// <param name="httpOnly"></param>
        public static void AddCookie(string name, string value, string domain, DateTime expires, bool httpOnly)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Domain = domain;
            cookie.Expires = expires;
            cookie.HttpOnly = httpOnly;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// cookie是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Contains(string name)
        {
            return HttpContext.Current.Request.Cookies[name] != null;
        }

        /// <summary>
        /// 获取cookie 的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValue(string name)
        {
            if (Contains(name))
                return HttpContext.Current.Request.Cookies[name].Value;
            return null;
        }

        /// <summary>
        /// 设置cookie，默认cookie域名是当前域名。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <param name="httpOnly"></param>
        public static void SetCookie(string name,string value,DateTime expires,bool httpOnly)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            string host = HttpContext.Current.Request.Url.Host;
            string domain = host;
            if (host.IndexOf(".") > -1)
                domain = host.Substring(host.IndexOf("."));
            cookie.Domain = domain; 
            cookie.Expires = expires;
            cookie.HttpOnly = httpOnly;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

    }
}
