using DataAccess;
using DataAccess.MIS;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UtilityLib;
using WEBAPP.Helper;

namespace WEBAPP.Areas.MIS.Controllers
{
    public class MISS02P001Controller : MISBaseController
    {
        #region Property
        private MISS02P001Model localModel = new MISS02P001Model();
        private MISS02P001Model TempModel
        {
            get
            {
                if (TempData["Model" + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData["Model" + SessionHelper.SYS_CurrentAreaController] = new MISS02P001Model();
                }
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
                return TempData["Model" + SessionHelper.SYS_CurrentAreaController] as MISS02P001Model;
            }
            set
            {
                TempData["Model" + SessionHelper.SYS_CurrentAreaController] = value;
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
            }
        }
        private MISS02P001Model TempSearch
        {
            get
            {
                if (TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = new MISS02P001Model();
                }
                TempData.Keep(StandardActionName.Search + SessionHelper.SYS_CurrentAreaController);

                return TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] as MISS02P001Model;
            }
            set { TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = value; }
        }

        #endregion

        #region Action 
        public ActionResult Index()
        {
            SetDefaulButton(StandardButtonMode.Index);
            if (TempSearch.IsDefaultSearch && !Request.GetRequest("page").IsNullOrEmpty())
            {
                localModel = TempSearch.CloneObject();

            }
            return View(StandardActionName.Index, localModel);
        }
        public ActionResult Search(MISS02P001Model model)
        {
            var da = new MISS02P001DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS02P001ExecuteType.GetAll;
            if (Request.GetRequest("page").IsNullOrEmpty())
            {
                model.IsDefaultSearch = true;
                TempSearch = model;
            }
            da.DTO.Model = TempSearch;
            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        [HttpPost]
        public ActionResult DeleteSearch(List<MISS02P001Model> data)
        {
            var jsonResult = new JsonResult();
            if (data != null && data.Count > 0)
            {
                var result = SaveData(StandardActionName.Delete, data);
                jsonResult = Success(result, StandardActionName.Delete);
            }
            else
            {
                jsonResult = ValidateError(StandardActionName.Delete, new ValidationError("", Translation.CenterLang.Center.DataNotFound));
            }
            return jsonResult;
        }
        [RuleSetForClientSideMessages("Add")]
        public ActionResult Add()
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData();
           
            return View(StandardActionName.Add, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCreate(MISS02P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                var result = SaveData(StandardActionName.SaveCreate, model);
                jsonResult = Success(result, StandardActionName.SaveCreate, Url.Action(StandardActionName.Index, new { page = 1 }));
            }
            else
            {
                jsonResult = ValidateError(ModelState, StandardActionName.SaveCreate);
            }

            return jsonResult;
        }
        [RuleSetForClientSideMessages("Edit")]
        public ActionResult Edit(MISS02P001Model model)
        {
            SetDefaulButton(StandardButtonMode.Modify);
            TempModel.YEAR = model.YEAR;
            localModel.YEAR = model.YEAR; 
            TempModel.COM_CODE = SessionHelper.SYS_COM_CODE;
            SetDefaultData();   //set ค่า DDL


            return View(StandardActionName.Edit, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveModify(MISS02P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                var result = SaveData(StandardActionName.SaveModify, model);
                jsonResult = Success(result, StandardActionName.SaveModify, Url.Action(StandardActionName.Index, new { page = 1 }));
            }
            else
            {
                jsonResult = ValidateError(ModelState, StandardActionName.SaveModify);
            }
            return jsonResult;
        }
        [HttpPost]
        public ActionResult DeleteDetail(List<MISS02P001Model> data)
        {
            var da = new MISS02P001DA();
            SetStandardErrorLog(da.DTO);
           // da.DTO.Execute.ExecuteType = MISS02P001ExecuteType.DeleteDetail;
            da.DTO.Model.COM_CODE = TempModel.COM_CODE;
            da.DTO.Model.YEAR = TempModel.YEAR;
            da.DTO.Models = (List<MISS02P001Model>)data;
            da.DeleteNoEF(da.DTO);

            return JsonAllowGet(da.DTO.Model.Details);
        }
        public ActionResult Bind_DetailAdd()
        {
            var da = new MISS02P001DA();
            SetStandardErrorLog(da.DTO);

            //da.DTO.Model.Details[0].DEPLOYMENT_DATE = date.AsDateTime();
            return JsonAllowGet(da.DTO.Model.Details);
        }
        public ActionResult Bind_Detail()
        {
            var da = new MISS02P001DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS02P001ExecuteType.GetDetailByID;
            da.DTO.Model.COM_CODE = TempModel.COM_CODE;
            da.DTO.Model.YEAR = TempModel.YEAR;

            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Model.Details);
        }
        [HttpGet]
        public ActionResult Info(string YEAR)
        {
            AddButton(StandButtonType.Button, "Back", Translation.CenterLang.Center.Back, iconCssClass: FaIcons.FaBackward, iconPosition: StandardIconPosition.BeforeText, url: Url.Action("Index"), isValidate: false, index: 0);

            localModel.YEAR = TempModel.YEAR = YEAR;
            
            return View(StandardActionName.Info, localModel);
        }
        public ActionResult SearchInfo()
        {
            var da = new MISS02P001DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS02P001ExecuteType.GetByID;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.DTO.Model.YEAR = TempModel.YEAR;
            da.SelectNoEF(da.DTO);

            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        #endregion

        #region Mehtod  
        //----------------------- DDL-----------------------
        private void SetDefaultData(string mode = "")
        {
            localModel.TYPE_DAY_MODEL = BindTypeDate();
        }

        private List<DDLCenterModel> BindTypeDate()
        {
            return GetDDLCenter(DDLCenterKey.DD_VSMS_FIX_TYPEDATE);
        }

        //----------------------------------------------//
        private DTOResult SaveData(string mode, object model)
        {
            var da = new MISS02P001DA();
            //ในกรณีที่มีการ SaveLog ให้ Include SetStandardLog ด้วย
            //SetStandardLog(
            //   da.DTO,
            //   model,
            //   GetSaveLogConfig("dbo", "VSMS_COMPANY", "COM_CODE"));


            if (mode == StandardActionName.SaveCreate)
            {
                SetStandardField(model);
                da.DTO.Model = (MISS02P001Model)model;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;

                da.InsertNoEF(da.DTO);
            }
            else if (mode == StandardActionName.SaveModify)
            {
                SetStandardField(model);
                da.DTO.Model = (MISS02P001Model)model;

                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                //da.DTO.Model.COM_BRANCH = TempModel.COM_BRANCH.TrimEnd();
                da.UpdateNoEF(da.DTO);
            }
            else if (mode == StandardActionName.Delete)
            {
                da.DTO.Models = (List<MISS02P001Model>)model;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.DeleteNoEF(da.DTO);
            }
            return da.DTO.Result;
        }

        #endregion
    }
}