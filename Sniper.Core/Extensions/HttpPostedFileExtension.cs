using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sniper.Core.Extensions
{
    public static class HttpPostedFileExtension
    {
        private static string[] img_exArr = new string[] { ".jpg,.jpeg,.gif,.png,.bmp" };

        /// <summary>
        /// 判断文件是否是图片
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        public static bool isImage(this HttpPostedFileBase postedFile)
        {
            string extension = System.IO.Path.GetExtension(postedFile.FileName);
            return img_exArr.Any(o => o.Contains(extension.ToLower()));
        }

        /// <summary>
        /// 文件大小超出范围
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="length">字节大小 例 100kb，100*1024 = 102400 </param>
        /// <returns></returns>
        public static bool isBigSize(this HttpPostedFileBase postedFile,int length)
        {
            return postedFile.ContentLength > length;
        }

    }
}
