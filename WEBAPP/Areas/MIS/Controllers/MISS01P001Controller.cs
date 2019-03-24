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
        public ActionResult Index()
        {
            SetDefaulButton(StandardButtonMode.Index);
            RemoveStandardButton("DeleteSearch");
            if (TempSearch.IsDefaultSearch && !Request.GetRequest("page").IsNullOrEmpty())
            {
                localModel = TempSearch.CloneObject();
            }

            localModel.USER_ID = SessionHelper.SYS_USER_ID;
            SetDefaultData(StandardActionName.Index);

            return View(StandardActionName.Index, localModel);
        }
        public ActionResult Info(MISS01P001Model model)
        {
            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetByID;
            da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
            da.DTO.Model.NO = model.NO;
            da.SelectNoEF(da.DTO);
            localModel = da.DTO.Model;

            SetDateToString(da.DTO.Model);
            SetDefaulButton(StandardButtonMode.Other);

            return View(StandardActionName.Info, localModel);
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
        [RuleSetForClientSideMessages("Add")]
        public ActionResult Add()
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData(StandardActionName.Add);
            localModel.USER_ID = SessionHelper.SYS_USER_ID;

            #region set default 
            localModel.MAN_PLM_DBA = 0;
            localModel.MAN_PLM_PL = 0;
            localModel.MAN_PLM_PRG = 0;
            localModel.MAN_PLM_QA = 0;
            localModel.MAN_PLM_SA = 0;
            #endregion

            return View(StandardActionName.Add, localModel);
        }
        public ActionResult GetNo(MISS01P001Model model)
        {
            var jsonResult = new JsonResult();

            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetNo;
            da.DTO.Model.COM_CODE = model.APP_CODE;
            da.SelectNoEF(da.DTO);
            da.DTO.Model.NO = da.DTO.Model.NO + 1;

            //jsonResult = Success(da.DTO.Result, StandardActionName.Add);

            return JsonAllowGet(da.DTO.Model);
            //return jsonResult;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCreate(MISS01P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                model = SetModelDateTime(model);
                model = RemoveSpace(model);
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
            da.DTO.Model.NO = model.NO;
            da.DTO.Model.APP_CODE = model.COM_CODE;
            da.SelectNoEF(da.DTO);
            localModel = da.DTO.Model;

            #region set default 
            var MenuPrgName = GetMenuPrgName(da.DTO.Model);
            localModel.MENU = MenuPrgName.MENU;
            localModel.PRG_NAME = MenuPrgName.PRG_NAME;
            #endregion

            SetDefaultData(StandardActionName.Edit);
            SetDateToString(da.DTO.Model);
            SetDefaulButton(StandardButtonMode.Modify);

            return View(StandardActionName.Edit, localModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveModify(MISS01P001Model model)
        {
            var jsonResult = new JsonResult();
            if (ModelState.IsValid)
            {
                model = RemoveSpace(model);
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
       
        private void SetDefaultData(string mode = "")
        {
            if (mode == "Add")
            {
                localModel.ISSUE_TYPE_MODEL = BindIssueType();
            }
            else if (mode == "Edit")
            {
                localModel.ISSUE_TYPE_MODEL = BindIssueType();
            }
            else if (mode == "Index")
            {
                localModel.STATUS_MODEL = BindStatus();
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
        private MISS01P001Model RemoveSpace(MISS01P001Model model)
        {
            if(!model.MENU.IsNullOrEmpty())
                model.MENU = model.MENU.Trim();
            if(!model.PRG_NAME.IsNullOrEmpty())
                model.PRG_NAME = model.PRG_NAME.Trim();

            return model;
        }
        private MISS01P001Model GetMenuPrgName(MISS01P001Model model)
        {
            var da = new MISS01P001DA();
            da.DTO.Execute.ExecuteType = MISS01P001ExecuteType.GetMenuPrgName;
            da.DTO.Model.NO = model.NO;
            da.DTO.Model.APP_CODE = model.APP_CODE;
            da.DTO.Model.ISSUE_BY = model.ISSUE_BY;
            da.SelectNoEF(da.DTO);

            return da.DTO.Model;
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
        private List<DDLCenterModel> BindStatus()
        {
            return GetDDLCenter(DDLCenterKey.DD_MISS01P001_004);
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
                da.UpdateNoEF(da.DTO);
            }

            return da.DTO.Result;
        }
        #endregion
    }
}