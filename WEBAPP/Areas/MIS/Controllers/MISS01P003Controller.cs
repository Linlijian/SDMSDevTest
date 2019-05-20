using DataAccess;
using DataAccess.MIS;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web.Mvc;
using UtilityLib;
using WEBAPP.Helper;
using System.Text;
using System.Web;

namespace WEBAPP.Areas.MIS.Controllers
{
    public class MISS01P003Controller : MISBaseController
    {
        #region Property
        private MISS01P003Model localModel = new MISS01P003Model();
        private MISS01P003Model TempModel
        {
            get
            {
                if (TempData["Model" + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData["Model" + SessionHelper.SYS_CurrentAreaController] = new MISS01P003Model();
                }
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
                return TempData["Model" + SessionHelper.SYS_CurrentAreaController] as MISS01P003Model;
            }
            set
            {
                TempData["Model" + SessionHelper.SYS_CurrentAreaController] = value;
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
            }
        }
        private MISS01P003Model TempSearch
        {
            get
            {
                if (TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = new MISS01P003Model();
                }
                TempData.Keep(StandardActionName.Search + SessionHelper.SYS_CurrentAreaController);

                return TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] as MISS01P003Model;
            }
            set { TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = value; }
        }
        #endregion

        #region Search 
        public ActionResult SearchAgree(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.GetAll;

            if (Request.GetRequest("page").IsNullOrEmpty())
            {
                model.IsDefaultSearch = true;
                TempSearch = model;
            }

            da.DTO.Model = TempSearch;
            da.DTO.Model.COM_CODE = model.APP_CODE;
            da.DTO.Model.STATUS = model.STATUS;
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        #endregion

        #region NewTask 
        public ActionResult Agree(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.ConfirmStatus;

            da.DTO.Model = model;
            da.DTO.Model.FALG = "A";
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.DTO.Model.CRET_DATE = DateTime.Now;
            da.UpdateNoEF(da.DTO);

            return JsonAllowGet(da.DTO.Model);
        }
        public ActionResult SearchNewTask(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.GetNewTask;

            if (Request.GetRequest("page").IsNullOrEmpty())
            {
                model.IsDefaultSearch = true;
                TempSearch = model;
            }

            da.DTO.Model = TempSearch;
            da.DTO.Model.COM_CODE = model.APP_CODE;
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        #endregion

        #region DoTask 
        public ActionResult SearchDoTask(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.GetDoTask;

            if (Request.GetRequest("page").IsNullOrEmpty())
            {
                model.IsDefaultSearch = true;
                TempSearch = model;
            }

            da.DTO.Model = TempSearch;
            da.DTO.Model.COM_CODE = model.APP_CODE;
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        [RuleSetForClientSideMessages("SolutionResult")]
        public ActionResult SolutionResult(MISS01P003Model model)
        {
            SetDefaulButton(StandardButtonMode.Other);
            SetDefaultData();
            AddButton(StandButtonType.ButtonComfirmAjax, "btnSAVESOLUTIONRESULT", Translation.CenterLang.Center.Save, iconCssClass: FaIcons.FaSave, url: Url.Action("SaveSolutionResult"), isValidate: true);

            #region set default 
            var view = string.Empty;
            view = "SolutionResult";

            var da = new MISS01P003DA();
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.SolutionResult;
            da.DTO.Model.COM_CODE = model.COM_CODE;
            da.DTO.Model.ISE_NO = model.ISE_NO;
            da.DTO.Model.SOLUTION = model.SOLUTION;
            da.SelectNoEF(da.DTO);

            localModel = da.DTO.Model;
            #endregion

            return View(view, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSolutionResult(MISS01P003Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                var result = SaveData("SolutionResult", model);
                jsonResult = Success(result, StandardActionName.SaveCreate, Url.Action(StandardActionName.Index, new { ACTIVE_STEP = 2 }));
            }
            else
            {
                jsonResult = ValidateError(ModelState, StandardActionName.SaveCreate);
            }

            return jsonResult;
        }
        #endregion

        #region DoneTask 
        public ActionResult Done(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.ConfirmStatus;

            da.DTO.Model = model;
            da.DTO.Model.FALG = "D";
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.DTO.Model.CRET_DATE = DateTime.Now;
            da.UpdateNoEF(da.DTO);


            return JsonAllowGet(da.DTO.Model);
        }
        public ActionResult Cancel(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.ConfirmStatus;

            da.DTO.Model = model;
            da.DTO.Model.FALG = "C";
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.DTO.Model.CRET_DATE = DateTime.Now;
            da.UpdateNoEF(da.DTO);


            return JsonAllowGet(da.DTO.Model);
        }
        public ActionResult SearchDoneTask(MISS01P003Model model)
        {
            var da = new MISS01P003DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.GetDoneTask;

            if (Request.GetRequest("page").IsNullOrEmpty())
            {
                model.IsDefaultSearch = true;
                TempSearch = model;
            }

            da.DTO.Model = TempSearch;
            da.DTO.Model.COM_CODE = model.APP_CODE;
            da.DTO.Model.CRET_BY = SessionHelper.SYS_USER_ID;
            da.SelectNoEF(da.DTO);
            return JsonAllowGet(da.DTO.Models, da.DTO.Result);
        }
        #endregion

        #region Action 
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
            SetDefaulButton(StandardButtonMode.Index);
            RemoveStandardButton("DeleteSearch");
            RemoveStandardButton("Add");

            if (ACTIVE_STEP == "1")
            {
                view = "NewTask";
                SetClientSideRuleSet("NewTask");

            }
            else if (ACTIVE_STEP == "2")
            {
                view = "DoTask";
                SetClientSideRuleSet("DoTask");

            }
            else if (ACTIVE_STEP == "3")
            {
                view = "DoneTask";
                SetClientSideRuleSet("DoneTask");
            }

            SetDefaultData();
            SetHeaderWizard(new WizardHelper.WizardHeaderConfig(
                ACTIVE_STEP,
                localModel.ACTIVE_STEP,
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P003.STEP_1, Url.Action("Index", new { ACTIVE_STEP = "1" }), iconCssClass: FaIcons.FaAreaChart),
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P003.STEP_2, Url.Action("Index", new { ACTIVE_STEP = "2" }), iconCssClass: FaIcons.FaFile),
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P003.STEP_3, Url.Action("Index", new { ACTIVE_STEP = "3" }), iconCssClass: FaIcons.FaFile)));

            return View(view, localModel);
        }
        #endregion

        #region Mehtod  
        private void SetDefaultData(string mode = "")
        {
            localModel.STATUS_MODEL = BindStatus();
            localModel.APP_CODE_MODEL = BindAppCode();
        }
        private List<DDLCenterModel> BindStatus()
        {
            return GetDDLCenter(DDLCenterKey.DD_MISS01P003_001);
        }
        private List<DDLCenterModel> BindAppCode()
        {
            return GetDDLCenter(DDLCenterKey.DD_APPLICATION);
        }
        private DTOResult SaveData(string mode, object model)
        {
            var da = new MISS01P003DA();

            //SetStandardField(model);
            da.DTO.Execute.ExecuteType = MISS01P003ExecuteType.UpdateSolutionResult;
            da.DTO.Model = (MISS01P003Model)model;
            da.UpdateNoEF(da.DTO);

            return da.DTO.Result;
        }
        #endregion
    }
}