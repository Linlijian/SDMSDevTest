﻿using DataAccess;
using DataAccess.MST;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UtilityLib;
using WEBAPP.Helper;

namespace WEBAPP.Areas.MST.Controllers
{
    public class MSTS03P001Controller : MSTBaseController
    {
        #region Property
        private MSTS03P001Model localModel = new MSTS03P001Model();
        private MSTS03P001Model TempModel
        {
            get
            {
                if (TempData["Model" + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData["Model" + SessionHelper.SYS_CurrentAreaController] = new MSTS03P001Model();
                }
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
                return TempData["Model" + SessionHelper.SYS_CurrentAreaController] as MSTS03P001Model;
            }
            set
            {
                TempData["Model" + SessionHelper.SYS_CurrentAreaController] = value;
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
            }
        }
        private MSTS03P001Model TempSearch
        {
            get
            {
                if (TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = new MSTS03P001Model();
                }
                TempData.Keep(StandardActionName.Search + SessionHelper.SYS_CurrentAreaController);

                return TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] as MSTS03P001Model;
            }
            set { TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = value; }
        }
        #endregion

        #region Action 
        public ActionResult Index()
        {
            SetDefaulButton(StandardButtonMode.Index);
            AddStandardButton(StandardButtonName.DownloadTemplate, url: "MISS02TP001");
            //AddStandardButton(StandardButtonName.LoadFile);
            AddStandardButton(StandardButtonName.Upload);
            if (TempSearch.IsDefaultSearch && !Request.GetRequest("page").IsNullOrEmpty())
            {
                localModel = TempSearch.CloneObject();
            }
            return View(StandardActionName.Index, localModel);
        }
        public ActionResult Search(MSTS03P001Model model)
        {
            var da = new MSTS03P001DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MSTS03P001ExecuteType.GetAll;
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
        public ActionResult DeleteSearch(List<MSTS03P001Model> data)
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
            localModel.COM_CODE = SessionHelper.SYS_COM_CODE;

            return View(StandardActionName.Add, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCreate(MSTS03P001Model model)
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
        public ActionResult Edit(MSTS03P001Model model)
        {
            SetDefaulButton(StandardButtonMode.Modify);

            SetDefaultData();   //set ค่า DDL


            return View(StandardActionName.Edit, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveModify(MSTS03P001Model model)
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
        #endregion

        #region Mehtod  
        //----------------------- DDL-----------------------
        private void SetDefaultData(string mode = "")
        {

        }

        private List<DDLCenterModel> BindTypeDate()
        {
            return GetDDLCenter(DDLCenterKey.DD_VSMS_FIX_TYPEDATE);
        }

        //----------------------------------------------//
        private DTOResult SaveData(string mode, object model)
        {
            var da = new MSTS03P001DA();
            //ในกรณีที่มีการ SaveLog ให้ Include SetStandardLog ด้วย
            //SetStandardLog(
            //   da.DTO,
            //   model,
            //   GetSaveLogConfig("dbo", "VSMS_COMPANY", "COM_CODE"));


            if (mode == StandardActionName.SaveCreate)
            {
                SetStandardField(model);
                da.DTO.Model = (MSTS03P001Model)model;
                da.DTO.Execute.ExecuteType = MSTS03P001ExecuteType.Insert;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;

                da.InsertNoEF(da.DTO);
            }
            else if (mode == StandardActionName.SaveModify)
            {
                SetStandardField(model);
                da.DTO.Execute.ExecuteType = MSTS03P001ExecuteType.Update;
                da.DTO.Model = (MSTS03P001Model)model;

                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                //da.DTO.Model.COM_BRANCH = TempModel.COM_BRANCH.TrimEnd();
                da.UpdateNoEF(da.DTO);
            }
            else if (mode == StandardActionName.Delete)
            {
                da.DTO.Models = (List<MSTS03P001Model>)model;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.DeleteNoEF(da.DTO);
            }
            else if (mode == "Upload")
            {
                da.DTO.Execute.ExecuteType = MSTS03P001ExecuteType.CallSPInsertExcel;
                SetStandardField(model);

                da.DTO.Model = (MSTS03P001Model)model;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;

                da.InsertNoEF(da.DTO);

                if (da.DTO.Result.IsResult)
                {
                    da.DTO.Execute.ExecuteType = MSTS03P001ExecuteType.ValidateExl;
                    da.UpdateNoEF(da.DTO);
                }
            }
            return da.DTO.Result;
        }

        #endregion
    }
}