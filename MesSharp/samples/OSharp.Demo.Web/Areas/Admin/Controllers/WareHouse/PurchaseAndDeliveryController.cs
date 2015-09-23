﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using Mes.Demo.Contracts.TestLog;
using Mes.Demo.Contracts.WareHouse;
using Mes.Demo.Dtos.WareHouse;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.TestLog;
using Mes.Demo.Models.WareHouse;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class PurchaseAndDeliveryController : AdminBaseController
    {
        public IWareHouseContract WareHouseContract { get; set; }

        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<PurchaseAndDelivery, int>(WareHouseContract.PurchaseAndDeliverys, out total, request).Select(m => new
            {
                m.Id,
                m.Model,
                m.Sn,
                m.AdType,
                m.CreatedTime,
                m.D2,
                m.D3,
                m.D4
            }).ToList().Select(m => new
            {
                m.Id,
                m.Model,
                m.Sn,
                AdType = m.AdType.ToDescription(),
                m.CreatedTime,
                m.D2,
                m.D3,
                m.D4
            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<PurchaseAndDeliveryDto>))] ICollection<PurchaseAndDeliveryDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = WareHouseContract.AddPurchaseAndDeliverys(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<PurchaseAndDeliveryDto>))] ICollection<PurchaseAndDeliveryDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = WareHouseContract.EditPurchaseAndDeliverys(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = WareHouseContract.DeletePurchaseAndDeliverys(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult InWareHouse(string model, string sn)
        {

            OperationResult result = WareHouseContract.InWareHouse(model, sn);
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutWareHouse( string sn)
        {
            OperationResult result = WareHouseContract.OutWareHouse(sn);
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public override void CreateExcel()
        {
            GridRequest request = new GridRequest(Request);
            var filterGroup = request.FilterGroup;
            Expression<Func<PurchaseAndDelivery, bool>> predicate = FilterHelper.GetExpression<PurchaseAndDelivery>(filterGroup);
            var purchaseAndDeliverys = WareHouseContract.PurchaseAndDeliverys.Where(predicate).Select(m => new
            {
                m.Model,
                m.Sn,
                m.AdType,
                m.CreatedTime,
                m.D2,
                m.D3,
                m.D4
            }).ToList().Select(m => new
            {
                m.Model,
                m.Sn,
                AdType = m.AdType.ToDescription(),
                退货时间=m.CreatedTime,
                进货时间=m.D2,
                二次退货时间=m.D3,
                二次进货时间=m.D4
            });
          
            Excel(purchaseAndDeliverys, "PurchaseAndDeliverys" + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }

        public override ActionResult Index()
        {
            ViewBag.ToolbarItem = "export";
            return View();
        }

        public ActionResult InAndOut()
        {
            return View();
        }
    }
}