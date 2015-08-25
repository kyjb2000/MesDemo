﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using System.Linq.Expressions;

using Mes.Core.Data;
using Mes.Demo.Dtos.SiteManagement;
using Mes.Demo.Models.SiteManagement;
using Mes.Utility.Data;


namespace Mes.Demo.Services.SiteManagement
{
    public partial class SiteManagementService
    {        
        public IRepository<ProblemSource, int> ProblemSourceRepository {protected get; set; }

        /// <summary>
        /// 获取部门 信息查询数据集
        /// </summary>
        public IQueryable<ProblemSource> ProblemSources { get { return ProblemSourceRepository.Entities; } }
        
        /// <summary>
        /// 检查部门信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的部门信息编号</param>
        /// <returns>部门信息是否存在</returns>
        public bool CheckProblemSourceExists(Expression<Func<ProblemSource, bool>> predicate, int id = 0)
        {
            return ProblemSourceRepository.CheckExists(predicate, id);
        }
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="dtos">要添加的部门信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddProblemSources(params ProblemSourceDto[] dtos)
        {
            return ProblemSourceRepository.Insert(dtos);
        }
        
        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="dtos">包含更新信息的部门DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditProblemSources(params ProblemSourceDto[] dtos)
        {
            return ProblemSourceRepository.Update(dtos);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="ids">要删除的部门信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteProblemSources(params int[] ids)
        {
            return ProblemSourceRepository.Delete(ids);
        }
    }
}
      
