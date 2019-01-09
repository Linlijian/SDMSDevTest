using DataAccess;
using DataAccess.MIS;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UtilityLib;
using WEBAPP.Helper;

namespace WEBAPP.Areas.MIS.Controllers
{
    public class MISS01P001Controller : MISBaseController
    {
        #region Property
        private MISS01P001Model localModel = new MISS01P001Model();
        private MISS01P001Model TempModel
        {
            get
            {
                if (TempData["Model" + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData["Model" + SessionHelper.SYS_CurrentAreaController] = new MISS01P001Model();
                }
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
                return TempData["Model" + SessionHelper.SYS_CurrentAreaController] as MISS01P001Model;
            }
            set
            {
                TempData["Model" + SessionHelper.SYS_CurrentAreaController] = value;
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
            }
        }
        private MISS01P001Model TempSearch
        {
            get
            {
                if (TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = new MISS01P001Model();
                }
                TempData.Keep(StandardActionName.Search + SessionHelper.SYS_CurrentAreaController);

                return TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] as MISS01P001Model;
            }
            set { TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = value; }
        }
        #endregion

        #region Action 
        [RuleSetForClientSideMessages("Wizard")]
        public ActionResult Index(string ACTIVE_STEP = "1")
        {
            //ViewBag.UrlToClosePage = Url.Action(StandardActionName.Index, "Default", new { Area = "Admin" });
            #region Set Close Page
            var menu = SessionHelper.SYS_MenuModel;
            if (menu != null)
            {
                var home = menu.Where(m => m.SYS_CODE.AsString().ToUpper() == "HOME").FirstOrDefault();
                if (home != null && AppExtensions.ExistsAction(home.PRG_ACTION, home.PRG_CONTROLLER, home.PRG_AREA))
                {
                    ViewBag.UrlToClosePage = Url.Action(home.PRG_ACTION, home.PRG_CONTROLLER, new { Area = home.PRG_AREA, SYS_SYS_CODE = home.SYS_CODE, SYS_PRG_CODE = home.PRG_CODE });
                }
            }
            #endregion

            var view = string.Empty;
            localModel.ACTIVE_STEP = "3"; //if set 2 then block step3 end
            if (ACTIVE_STEP == "1")
            {
                view = "Step1";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Step1");

            }
            else if (ACTIVE_STEP == "2")
            {
                view = "Step2";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Step2");

            }
            else if (ACTIVE_STEP == "3")
            {
                view = "Step3";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Step3");

            }

            SetHeaderWizard(new WizardHelper.WizardHeaderConfig(
                ACTIVE_STEP,
                localModel.ACTIVE_STEP,
                new WizardHelper.WizardHeader("", Url.Action("Index", new { ACTIVE_STEP = "1" }), iconCssClass: FaIcons.FaAreaChart, textStep: Translation.MIS.MISS01P001.STEP_1),
                new WizardHelper.WizardHeader("", Url.Action("Index", new { ACTIVE_STEP = "2" }), iconCssClass: FaIcons.FaFile, textStep: Translation.MIS.MISS01P001.STEP_2),
                new WizardHelper.WizardHeader("", Url.Action("Index", new { ACTIVE_STEP = "3" }), iconCssClass: FaIcons.FaFile, textStep: Translation.MIS.MISS01P001.STEP_3)));

            return View(view, localModel);
        }
        public ActionResult Search(MISS01P001Model model)
        {
            var da = new MISS01P001DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetAll;
            
            if (Request.GetRequest("page").IsNullOrEmpty())
            {
                model.IsDefaultSearch = true;
                TempSearch = model;
            }
            da.DTO.Model = TempSearch;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        [HttpPost]
        public ActionResult DeleteSearch(List<MISS01P001Model> data)
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
        public ActionResult Add()
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData();
            localModel.COM_CODE = SessionHelper.SYS_COM_CODE;

            return View(StandardActionName.Add, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCreate(MISS01P001Model model)
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
        public ActionResult Edit(MISS01P001Model model)
        {
            
            return View(StandardActionName.Edit, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveModify(MISS01P001Model model)
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
            localModel.DEFECT_MODEL = BindDefect();
            localModel.PRIORITY_MODEL = BindPriority();
            localModel.ISSUE_TYPE_MODEL = BindIssueType();
        }
        private List<DDLCenterModel> BindIssueType()
        {
            return GetDDLCenter(DDLCenterKey.DD_MISS01P001_001, new VSMParameter(SessionHelper.SYS_COM_CODE.Trim()));
        }
        private List<DDLCenterModel> BindDefect()
        {
            return GetDDLCenter(DDLCenterKey.DD_MISS01P001_002, new VSMParameter(SessionHelper.SYS_COM_CODE.Trim()));
        }
        private List<DDLCenterModel> BindPriority()
        {
            return GetDDLCenter(DDLCenterKey.DD_MISS01P001_003, new VSMParameter(SessionHelper.SYS_COM_CODE.Trim()));
        }
        //----------------------------------------------//
        private DTOResult SaveData(string mode, object model)
        {
            var da = new MISS01P001DA();
            //ในกรณีที่มีการ SaveLog ให้ Include SetStandardLog ด้วย
            //SetStandardLog(
            //   da.DTO,
            //   model,
            //   GetSaveLogConfig("dbo", "VSMS_COMPANY", "COM_CODE"));


            if (mode == StandardActionName.SaveCreate)
            {
                SetStandardField(model);
                da.DTO.Model = (MISS01P001Model)model;
                da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.Insert;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;

                da.InsertNoEF(da.DTO);
            }
            else if (mode == StandardActionName.SaveModify)
            {
                SetStandardField(model);
                da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.Update;
                da.DTO.Model = (MISS01P001Model)model;

                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                //da.DTO.Model.COM_BRANCH = TempModel.COM_BRANCH.TrimEnd();
                da.UpdateNoEF(da.DTO);
            }
            else if (mode == StandardActionName.Delete)
            {
                da.DTO.Models = (List<MISS01P001Model>)model;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.DeleteNoEF(da.DTO);
            }
           
            return da.DTO.Result;
        }
        private void SetButton(string ACTIVE_STEP = "")
        {
            SetDefaulButton(StandardButtonMode.Other);
            if (ACTIVE_STEP == "1")
            {
                SetDefaulButton(StandardButtonMode.Index);
            }
            else if (ACTIVE_STEP == "2")
            {
                AddStandardButton(StandardButtonName.Search);
            }
            else if (ACTIVE_STEP == "3")
            {
                AddStandardButton(StandardButtonName.Search);
            }
        }
        #endregion
    }
}