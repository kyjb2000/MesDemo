﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core;
using Mes.Core.Data;
using Mes.Demo.Contracts;
using Mes.Demo.Models.Identity;


namespace Mes.Demo.Services
{
    /// <summary>
    /// 业务实现——身份认证模块
    /// </summary>
    public partial class IdentityService : ServiceBase, IIdentityContract
    {
        public IdentityService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// 获取或设置 组织机构信息仓储操作对象
        /// </summary>
        public IRepository<Organization, int> OrganizationRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 角色信息仓储对象
        /// </summary>
        public IRepository<Role, int> RoleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户信息仓储对象
        /// </summary>
        public IRepository<User, int> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户扩展信息仓储对象
        /// </summary>
        public IRepository<UserExtend, int> UserExtendRepository { protected get; set; }
    }
}