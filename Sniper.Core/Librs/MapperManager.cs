using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Librs
{
   public class MapperManager
    {
        /// <summary>
        /// 数据映射转换
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TTo Map<TFrom, TTo>(TFrom obj)
        {
            return EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>().Map(obj);
        }

        /// <summary>
        /// 数据映射转换 List
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="enumrable"></param>
        /// <returns>List</returns>
        public static List<TTo> MaList<TFrom, TTo>(IEnumerable<TFrom> enumrable)
        {
            return EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>().MapEnum(enumrable).ToList();
        }

        /// <summary>
        /// 数据映射转换 IEnumerable
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="enumrable"></param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<TTo> MaEnum<TFrom, TTo>(IEnumerable<TFrom> enumrable)
        {
            return EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>().MapEnum(enumrable);
        }


    }
}
