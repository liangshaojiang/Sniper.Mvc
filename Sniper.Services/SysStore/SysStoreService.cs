using Sniper.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysStore;
using Sniper.Core.Caching;
using Sniper.Mapping;

namespace Sniper.Services.SysStore
{
    public class SysStoreService : ISysStoreService
    {
        private const string MODEL_KEY = "Sniper.services.sysstore";
        private IRepository<Entities.SysStore> _sysStoreRepository;
        private ICacheManager _cacheManager;

        public SysStoreService(IRepository<Entities.SysStore> sysStoreRepository,
            ICacheManager cacheManager)
        {
            this._sysStoreRepository = sysStoreRepository;
            this._cacheManager = cacheManager;
        }

        /// <summary>
        /// 保存系统名称设置
        /// </summary>
        /// <returns></returns>
        public SysStoreMapping getStore()
        { 
            return _cacheManager.get<SysStoreMapping>(MODEL_KEY, () =>
            {
                var store = _sysStoreRepository.Table.SingleOrDefault();
                if (store != null)
                    return store.toModel();
                return null;
            });
        }

        /// <summary>
        /// 保存系统名称设置
        /// </summary>
        /// <param name="model"></param>
        public void saveStore(SysStoreMapping model)
        {
            var item = _sysStoreRepository.Table.SingleOrDefault();
            if (item == null)
            {
                model.Id = Guid.NewGuid();
                model.CreationTime = DateTime.Now;
                item = model.toEntity();
                _sysStoreRepository.insert(item);
            }
            else
            {
                item.Name = model.Name;
                item.SslEnabled = model.SslEnabled;
                item.IconClass = model.IconClass;
                item.Disabled = model.Disabled;
                item.ModifiedTime = DateTime.Now;
                item.Modifier = model.Modifier;
                item.CompanyName = model.CompanyName;
                _sysStoreRepository.update(item);
                _cacheManager.remove(MODEL_KEY);
            }
        }
    }

}
