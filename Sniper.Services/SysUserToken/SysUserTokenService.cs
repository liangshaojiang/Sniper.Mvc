using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Core.Caching;
using Sniper.Core.Data;
using Sniper.Entities;
using Sniper.Mapping.SysUserToken;

namespace Sniper.Services.SysUserToken
{
    public class SysUserTokenService : ISysUserTokenService
    {
        //缓存key
        private const string MODEL_KEY = "Sniper.Admin.logged_token_{0}";

        private IRepository<Entities.SysUserToken> _sysUserTokenRepository;
        private ICacheManager _cacheManager;

        public SysUserTokenService(IRepository<Entities.SysUserToken> sysUserTokenRepository,
            ICacheManager cacheManager)
        {
            this._sysUserTokenRepository = sysUserTokenRepository;
            this._cacheManager = cacheManager;
        }

        /// <summary>
        /// 已登录用户获取token
        /// 获取到后并缓存
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public SysUserTokenMapping getLoggedToken(Guid tokenId)
        {
            string key = String.Format(MODEL_KEY, tokenId);
            return _cacheManager.get<SysUserTokenMapping>(key, () =>
            {
                var item = _sysUserTokenRepository.getById(tokenId);
                if (item != null)
                    return Core.Librs.MapperManager.Map<Entities.SysUserToken, SysUserTokenMapping>(item);
                return null;
            });
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="token"></param>
        public void insertToken(Entities.SysUserToken token)
        {
            var list = _sysUserTokenRepository.Table.Where(o => o.SysUserId == token.SysUserId && o.ExpireTime < DateTime.Now).ToList();
            if (list.Any())
                foreach (var del in list)
                    _sysUserTokenRepository.Entities.Remove(del);
            _sysUserTokenRepository.insert(token);
        }
    }
}
