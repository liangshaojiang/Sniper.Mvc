using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sniper.Mapping.SysUser;
using Sniper.Admin.Framework.Security;
using Sniper.Mapping.SysStore;
using Sniper.Services.SysStore;

namespace Sniper.Admin.Framework
{
    public class WebWorkContext : Infrastructure.IWorkContext
    {
        private IAuthenticationService _authenticationService;
        private ISysStoreService _sysStoreService;

        public WebWorkContext(IAuthenticationService authenticationService,
            ISysStoreService sysStoreService)
        {
            this._authenticationService = authenticationService;
            this._sysStoreService = sysStoreService;
        }

        /// <summary>
        /// 当前登录用户对象
        /// </summary>
        public SysUserMapping CurrentUser
        {
            get
            {
              return _authenticationService.getAuthenticatedSysUser();
            }
        }

        /// <summary>
        /// 系统对象
        /// </summary>
        public SysStoreMapping Store
        {
            get
            {
                return _sysStoreService.getStore();
            }
        }
    }
}
