using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Sniper.Core
{
    public class WebHelper
    {

        /// <summary>
        /// 是否是无效的请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected static bool isRequestInvalid()
        {
            var context = HttpContext.Current;
            if (context == null)
                return true;
            try
            {
                if (context.Request == null)
                    return true;
            }
            catch (HttpException)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改配置文件时间
        /// </summary>
        /// <returns></returns>
        public static bool tryWriteWebConfig()
        {
            try
            {
                //web.config时间
                File.SetLastWriteTimeUtc(mapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取客户端ip地址
        /// </summary>
        /// <returns></returns>
        public static string getClientIpAddress()
        {
            if (isRequestInvalid())
                return string.Empty;
            //获取远程主机ip地址 
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            if (String.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //最后判断获取是否成功，并检查IP地址的格式
            if (!String.IsNullOrEmpty(userHostAddress) && isIpAddress(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        ///  判断是否是ip地址
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool isIpAddress(string ipAddress)
        {
            if (isRequestInvalid())
                return false;
            if (String.IsNullOrEmpty(ipAddress))
                return false;
            return System.Text.RegularExpressions.Regex.IsMatch(ipAddress, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获取由浏览器在 User-Agent 请求标头中发送的浏览器字符串（如果有）
        /// </summary>
        /// <returns></returns>
        public static string getBrowserAgent()
        {
            return HttpContext.Current.Request.UserAgent;
        }

        /// <summary>
        /// 获取来源Referrer的绝对路径
        /// </summary>
        /// <returns></returns>
        public static string getUrlReferrer()
        {
            if (isRequestInvalid())
                return string.Empty;
            string referrerUrl = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer != null)
                referrerUrl = HttpContext.Current.Request.UrlReferrer.PathAndQuery;

            return referrerUrl;
        }

        /// <summary>
        /// 获取来源Referrer完整的url
        /// </summary>
        /// <returns></returns>
        public static string getReferrerOriginalString()
        {
            if (isRequestInvalid())
                return string.Empty;
            string referrerUrl = string.Empty;

            if (HttpContext.Current.Request.UrlReferrer != null)
                referrerUrl = HttpContext.Current.Request.UrlReferrer.OriginalString;

            return referrerUrl;
        }

        /// <summary>
        /// 获取 请求的RawUrl（虚拟目录名+页面名+参数）
        /// </summary>
        /// <returns></returns>
        public static string getRawUrl()
        {
            if (isRequestInvalid())
                return string.Empty;
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 获取请求的完整Url
        /// </summary>
        /// <returns></returns>
        public static string getUrl()
        {
            if (isRequestInvalid())
                return string.Empty;
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 路劲转为绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string mapPath(string path)
        {
            if (!isRequestInvalid())
                return HttpContext.Current.Server.MapPath(path);
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        /// <summary>
        /// 是不是https安全请求
        /// </summary>
        /// <returns></returns>
        public static bool isSecureConnection()
        {
            if (isRequestInvalid())
                return false;
            return HttpContext.Current.Request.IsSecureConnection;
        }

        /// <summary>
        /// 转换获取当前的连接完整路径地址
        /// </summary>
        /// <param name="includeQueryString">指示包含查询字符串</param>
        /// <param name="useSsl">https连接</param>
        /// <returns></returns>
        public static string getThisPageUrl(bool includeQueryString, bool useSsl)
        {
            string url = string.Empty;
            if (isRequestInvalid())
                return url;
            //带参数时才启用安全连接
            if (includeQueryString)
            {
                string storeHost = getHost(useSsl);
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                url = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                if (HttpContext.Current.Request.Url != null)
                    url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            return url.ToLowerInvariant();
        }

        /// <summary>
        /// 获取主机地址 带http头或https头
        /// </summary>
        /// <param name="useSsl">指示安全连接</param>
        /// <returns></returns>
        public static string getHost(bool useSsl)
        {
            var result = "";
            var httpHost = serverVariables("HTTP_HOST");
            if (!String.IsNullOrEmpty(httpHost))
            {
                result = "http://" + httpHost;
                if (!result.EndsWith("/"))
                    result += "/";
            }
            if (useSsl)
                result = result.Replace("http:/", "https:/");
            return result.ToLowerInvariant();
        }

        /// <summary>
        /// 获取 Web 服务器变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string serverVariables(string name)
        {
            string result = string.Empty;
            if (isRequestInvalid())
                return result;
            if (HttpContext.Current.Request.ServerVariables[name] != null)
                result = HttpContext.Current.Request.ServerVariables[name];
            return result;
        }

    }
}
