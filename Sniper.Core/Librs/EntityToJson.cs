using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Librs
{
    public class EntityToJson
    {
        /// <summary>
        /// entity 类型转换成json格式的log日志字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EntityToJsonLog<T>(T value) where T : class
        {
            StringBuilder sb = new StringBuilder();
            Type t = value.GetType();
            System.Reflection.PropertyInfo[] propertyInfos = t.GetProperties();
            sb.Append("{");
            foreach (var p in propertyInfos)
            {
                if (p.PropertyType.IsSerializable)
                {
                    object obj = p.GetValue(value, null);
                    if (obj != null)
                        sb.Append("\"" + p.Name + "\"" + ":" + "\"" + obj.ToString() + "\"");
                    else
                        sb.Append("\"" + p.Name + "\"" + ":" + "null");
                    sb.Append(",");
                }
            }
            return sb.ToString().TrimEnd(',') + "}";
        }
    }
}
