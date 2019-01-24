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
        public ActionResult SearchAssign(MISS01P001Model model)
        {
            var da = new MISS01P001DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetAllAssign;

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
        [RuleSetForClientSideMessages("Assignment")]
        public ActionResult Assignment(MISS01P001Model model)
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData();
            SetButton("Assignment");

            #region set default 
            var view = string.Empty;
            view = "Assignment";

            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetAssignment;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.DTO.Model.NO = model.NO;
            da.SelectNoEF(da.DTO);

            localModel = da.DTO.Model;
            //localModel.COM_CODE = SessionHelper.SYS_COM_CODE;
            #endregion

            return View(view, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAssign(MISS01P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                var result = SaveData("SaveAssign", model);
                jsonResult = Success(result, StandardActionName.SaveCreate, Url.Action(StandardActionName.Index, new { ACTIVE_STEP = 2 }));
            }
            else
            {
                jsonResult = ValidateError(ModelState, StandardActionName.SaveCreate);
            }

            return jsonResult;
        }
        [RuleSetForClientSideMessages("FilePacket")]
        public ActionResult FilePacket(MISS01P001Model model)
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData();
            SetButton("FilePacket");

            #region set default 
            var view = string.Empty;
            view = "FilePacket";

            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetFilePacket;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.DTO.Model.NO = model.NO;
            da.SelectNoEF(da.DTO);

            localModel = da.DTO.Model;
            //localModel.COM_CODE = SessionHelper.SYS_COM_CODE;
            #endregion

            return View(view, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFilePacket(MISS01P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                var result = SaveData("FilePacket", model);
                jsonResult = Success(result, StandardActionName.SaveCreate, Url.Action(StandardActionName.Index, new { ACTIVE_STEP = 2 }));
            }
            else
            {
                jsonResult = ValidateError(ModelState, StandardActionName.SaveCreate);
            }

            return jsonResult;
        }
        [RuleSetForClientSideMessages("Add")]
        public ActionResult Add()
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData(StandardActionName.Add);
            localModel.COM_CODE = SessionHelper.SYS_COM_CODE;

            #region set default 
            localModel.MAN_PLM_DBA = 0;
            localModel.MAN_PLM_PL = 0;
            localModel.MAN_PLM_PRG = 0;
            localModel.MAN_PLM_QA = 0;
            localModel.MAN_PLM_SA = 0;
            #endregion

            #region set running number 
            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetNo;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.SelectNoEF(da.DTO);
            localModel.NO = da.DTO.Model.NO + 1;
            #endregion

            return View(StandardActionName.Add, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCreate(MISS01P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                model = SetModelDateTime(model);
                var result = SaveData(StandardActionName.SaveCreate, model);
                //if(result.ResultMsg != null)
                //{
                //    string msg = string.Format("Issue no. {0}" + Environment.NewLine +
                //             "FROM {1}" + Environment.NewLine +
                //             "TO " + "@" + "{2}" + Environment.NewLine +
                //             "Detail {3}", model.NO, model.ISSUE_BY, model.SOLUTION, model.REMARK);

                //    lineNotify(msg);
                //}
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
            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetByID;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.DTO.Model.NO = model.NO;
            da.SelectNoEF(da.DTO);
            localModel = da.DTO.Model;

            SetDateToString(da.DTO.Model);

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
        private void lineNotify(string msg)
        {
            string token = "RdPADiB93gXjdR2l2QylH3Xh3f9bRmmIR8ijihQqMkV";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", msg);
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);

                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Mehtod  
        //----------------------- DDL-----------------------
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
            else if (ACTIVE_STEP == "Assignment")
            {
                AddButton(StandButtonType.ButtonComfirmAjax, "btnSAVEASSIGN", Translation.MIS.MISS01P001.SAVEASSIGN, iconCssClass: FaIcons.FaCopy, url: Url.Action("SaveAssign"), isValidate: true);
            }
            else if (ACTIVE_STEP == "FilePacket")
            {
                AddButton(StandButtonType.ButtonComfirmAjax, "btnSAVEFILEPACKET", Translation.MIS.MISS01P001.SAVEASSIGN, iconCssClass: FaIcons.FaCopy, url: Url.Action("SaveFilePacket"), isValidate: true);
            }
        }
        private void SetDefaultData(string mode = "")
        {
            if (mode == "1")
            {  
            }
            else if (mode == "2")
            {
                localModel.COM_CODE = SessionHelper.SYS_COM_CODE;
            }
            else if (mode == "3")
            {
            }
            else if (mode == "Add")
            {
                localModel.ISSUE_TYPE_MODEL = BindIssueType();
            }
        }
        private void SetDateToString(MISS01P001Model model)
        {
            if (!model.CLOSE_DATE.IsNullOrEmpty())
                localModel.STR_CLOSE_DATE = model.CLOSE_DATE.AsStringDate();
            if (!model.DEPLOY_QA.IsNullOrEmpty())
                localModel.STR_DEPLOY_QA = model.DEPLOY_QA.AsStringDate();
            if (!model.DEPLOY_PD.IsNullOrEmpty())
                localModel.STR_RDEPLOY_PD = model.DEPLOY_PD.AsStringDate();
            if (!model.ISSUE_DATE.IsNullOrEmpty())
                localModel.STR_ISSUE_DATE = model.ISSUE_DATE.AsStringDate();
            if (!model.RESPONSE_DATE.IsNullOrEmpty())
                localModel.STR_RESPONSE_DATE = model.RESPONSE_DATE.AsStringDate();
            if (!model.TARGET_DATE.IsNullOrEmpty())
                localModel.STR_TARGET_DATE = model.TARGET_DATE.AsStringDate();
        }
        private MISS01P001Model SetModelDateTime(MISS01P001Model model)
        {
            if (!model.STR_CLOSE_DATE.IsNullOrEmpty())
                model.CLOSE_DATE = model.STR_CLOSE_DATE.AsDateTimes();
            if (!model.STR_DEPLOY_QA.IsNullOrEmpty())
                model.DEPLOY_QA = model.STR_DEPLOY_QA.AsDateTimes();
            if (!model.STR_RDEPLOY_PD.IsNullOrEmpty())
                model.DEPLOY_PD = model.STR_RDEPLOY_PD.AsDateTimes();
            if (!model.STR_ISSUE_DATE.IsNullOrEmpty())
                model.ISSUE_DATE = model.STR_ISSUE_DATE.AsDateTimes();
            if (!model.STR_RESPONSE_DATE.IsNullOrEmpty())
                model.RESPONSE_DATE = model.STR_RESPONSE_DATE.AsDateTimes();
            if (!model.STR_TARGET_DATE.IsNullOrEmpty())
                model.TARGET_DATE = model.STR_TARGET_DATE.AsDateTimes();
            return model;
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
            else if (mode == "SaveAssign")
            {
                SetStandardField(model);
                da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.UpdateAssignment;
                da.DTO.Model = (MISS01P001Model)model;

                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.UpdateNoEF(da.DTO);
            }
            else if (mode == "FilePacket")
            {
                SetStandardField(model);
                da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.UpdateFilePacket;
                da.DTO.Model = (MISS01P001Model)model;

                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.UpdateNoEF(da.DTO);
            }

            return da.DTO.Result;
        }
        #endregion
    }
}