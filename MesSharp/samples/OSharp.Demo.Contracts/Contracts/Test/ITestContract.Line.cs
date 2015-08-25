﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using System.Linq.Expressions;

using Mes.Core;
using Mes.Demo.Dtos.Test;
using Mes.Demo.Models.Test;
using Mes.Utility.Data;


namespace Mes.Demo.Contracts.Test
{
	public partial interface ITestContract:IDependency
    {
        /// <summary>
        /// 获取产线 信息查询数据集
        /// </summary>
        IQueryable<Line> Lines { get; }
        
        /// <summary>
        /// 检查产线信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的产线信息编号</param>
        /// <returns>产线信息是否存在</returns>
        bool CheckLineExists(Expression<Func<Line, bool>> predicate, int id = 0);
        
        /// <summary>
        /// 添加产线信息
        /// </summary>
        /// <param name="dtos">要添加的产线信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddLines(params LineDto[] dtos);
        
        /// <summary>
        /// 更新产线信息
        /// </summary>
        /// <param name="dtos">包含更新信息的产线DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditLines(params LineDto[] dtos);

        /// <summary>
        /// 删除产线信息
        /// </summary>
        /// <param name="ids">要删除的产线信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteLines(params int[] ids);
    }
}
      
