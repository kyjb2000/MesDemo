﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Mes.Demo.Contracts.SiteManagement;
using Mes.Demo.Dtos.SiteManagement;
using Mes.Demo.Models.SiteManagement;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class ProblemTypeController : AdminBaseController
    {
        public ISiteManagementContract SiteManagementContract { get; set; }



        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<ProblemType, int>(SiteManagementContract.ProblemTypes, out total, request).Select(m => new
            {
                m.Id,
                m.Text,
                m.Value,
                m.CreatedTime,

            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<ProblemTypeDto>))] ICollection<ProblemTypeDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SiteManagementContract.AddProblemTypes(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<ProblemTypeDto>))] ICollection<ProblemTypeDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = SiteManagementContract.EditProblemTypes(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = SiteManagementContract.DeleteProblemTypes(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

    }
}
