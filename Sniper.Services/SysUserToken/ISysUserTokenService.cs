using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysUserToken;

namespace Sniper.Services.SysUserToken
{
    public interface ISysUserTokenService
    {
        /// <summary>
        /// 插入登录token
        /// </summary>
        /// <param name="token"></param>
        void insertToken(Entities.SysUserToken token);
         
        /// <summary>
        /// 已登录用户获取token
        /// 获取到后并缓存
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        SysUserTokenMapping getLoggedToken(Guid tokenId);

    }
}
