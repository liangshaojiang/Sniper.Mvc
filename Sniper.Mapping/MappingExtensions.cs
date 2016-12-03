using Sniper.Core.Librs;
using Sniper.Mapping.SysPermission;
using Sniper.Mapping.SysRole;
using Sniper.Mapping.SysStore;
using Sniper.Mapping.SysUser;
using Sniper.Mapping.SysUserLoginLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Mapping
{
   public static class MappingExtensions
    {
        private static TDestination mapTo<TSource, TDestination>(this TSource source)
        {
            return MapperManager.Map<TSource, TDestination>(source);
        }
         

        #region SysUser

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Entities.SysUser toEntity(this SysUserMapping model)
        {
            return mapTo<SysUserMapping, Entities.SysUser>(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SysUserMapping toModel(this Entities.SysUser source)
        {
            return mapTo<Entities.SysUser,SysUserMapping>(source);
        }
        #endregion

        #region SysRole

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Entities.SysRole toEntity(this SysRoleMapping model)
        {
            return mapTo<SysRoleMapping, Entities.SysRole>(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SysRoleMapping toModel(this Entities.SysRole source)
        {
            return mapTo<Entities.SysRole, SysRoleMapping>(source);
        }
        #endregion

        #region SysUserLoginLog

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Entities.SysUserLoginLog toEntity(this SysUserLoginLogMapping model)
        {
            return mapTo<SysUserLoginLogMapping, Entities.SysUserLoginLog>(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SysUserLoginLogMapping toModel(this Entities.SysUserLoginLog source)
        {
            return mapTo<Entities.SysUserLoginLog, SysUserLoginLogMapping>(source);
        }

        #endregion

        #region SysPermission

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Entities.SysPermission toEntity(this PermissionRecordMapping model)
        {
            return mapTo<PermissionRecordMapping, Entities.SysPermission>(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static PermissionRecordMapping toModel(this Entities.SysPermission source)
        {
            return mapTo<Entities.SysPermission, PermissionRecordMapping>(source);
        }
        #endregion

        #region SysStore

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Entities.SysStore toEntity(this SysStoreMapping model)
        {
            return mapTo<SysStoreMapping, Entities.SysStore>(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SysStoreMapping toModel(this Entities.SysStore source)
        {
            return mapTo<Entities.SysStore, SysStoreMapping>(source);
        }
        #endregion
    }
}
