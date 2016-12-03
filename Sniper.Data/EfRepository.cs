using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Data;

namespace Sniper.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private DbContext _dbcontext;
        private IDbSet<TEntity> _entities;

        public EfRepository(DbContext dbContext)
        {
            this._dbcontext = dbContext;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        public DbContext DbContext
        {
            get
            {
                return _dbcontext;
            }
        }

        /// <summary>
        /// 实体集
        /// </summary>
        public IDbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbcontext.Set<TEntity>();
                return _entities;
            }
        }

        /// <summary>
        /// 表
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="exc">实体验证错误对象</param>
        /// <returns></returns>
        protected string getFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("属性: {0} 错误: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("删除的实体entity为空");
                this.Entities.Remove(entity);
                _dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(getFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity getById(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        public void insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("插入的实体entity为空");
                this.Entities.Add(entity);
                this._dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(getFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("更新的实体entity为空");
                this._dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(getFullErrorText(dbEx), dbEx);
            }
        }



    }
}
