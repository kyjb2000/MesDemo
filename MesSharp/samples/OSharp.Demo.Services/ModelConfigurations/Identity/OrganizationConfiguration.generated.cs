﻿// <autogenerated>
//   This file was generated by T4 code generator ModelCodeScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;

using Mes.Core.Data.Entity;

using Mes.Demo.Models.Identity;


namespace Mes.Demo.ModelConfigurations.Identity
{
    /// <summary>
    /// 实体类-数据表映射——Organization
    /// </summary> 
	public partial class OrganizationConfiguration : EntityConfigurationBase<Organization, Int32>
    { 
        /// <summary>
        /// 初始化一个<see cref="OrganizationConfiguration"/>类型的新实例
        /// </summary>
        public OrganizationConfiguration()
        {
            OrganizationConfigurationAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void OrganizationConfigurationAppend();
    }
}
