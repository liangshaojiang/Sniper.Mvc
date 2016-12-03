using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Data
{
    public partial interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// DbContext
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// 实体
        /// </summary>
        IDbSet<TEntity> Entities { get; }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity getById(object id);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void insert(TEntity entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void update(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void delete(TEntity entity);

        /// <summary>
        /// 表
        /// </summary>
        IQueryable<TEntity> Table { get; }
    }
}
