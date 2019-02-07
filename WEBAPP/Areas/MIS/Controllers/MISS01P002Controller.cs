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
    public class MISS01P002Controller : MISBaseController
    {
        #region Property
        private MISS01P002Model localModel = new MISS01P002Model();
        private MISS01P002Model TempModel
        {
            get
            {
                if (TempData["Model" + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData["Model" + SessionHelper.SYS_CurrentAreaController] = new MISS01P002Model();
                }
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
                return TempData["Model" + SessionHelper.SYS_CurrentAreaController] as MISS01P002Model;
            }
            set
            {
                TempData["Model" + SessionHelper.SYS_CurrentAreaController] = value;
                TempData.Keep("Model" + SessionHelper.SYS_CurrentAreaController);
            }
        }
        private MISS01P002Model TempSearch
        {
            get
            {
                if (TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] == null)
                {
                    TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] = new MISS01P002Model();
                }
                TempData.Keep(StandardActionName.Search + SessionHelper.SYS_CurrentAreaController);

                return TempData[StandardActionName.Search + SessionHelper.SYS_CurrentAreaController] as MISS01P002Model;
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
            return View(StandardActionName.Index, localModel);
        }
        public ActionResult SearchAssign(MISS01P002Model model)
        {
            var da = new MISS01P002DA();
            SetStandardErrorLog(da.DTO);
            da.DTO.Execute.ExecuteType = MISS01P002ExecuteType.GetAllAssign;

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
        [RuleSetForClientSideMessages("Assignment")]
        public ActionResult Assignment(MISS01P002Model model)
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData();
            SetButton("Assignment");

            #region set default 
            var view = string.Empty;
            view = "Assignment";

            var da = new MISS01P002DA();
            da.DTO.Execute.ExecuteType = MISS01P002ExecuteType.GetAssignment;
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
        public ActionResult SaveAssign(MISS01P002Model model)
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
        public ActionResult FilePacket(MISS01P002Model model)
        {
            SetDefaulButton(StandardButtonMode.Create);
            SetDefaultData();
            SetButton("FilePacket");

            #region set default 
            var view = string.Empty;
            view = "FilePacket";

            var da = new MISS01P002DA();
            da.DTO.Execute.ExecuteType = MISS01P002ExecuteType.GetFilePacket;
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
        public ActionResult SaveFilePacket(MISS01P002Model model)
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
        #endregion

        #region Mehtod  
        //----------------------- DDL-----------------------

        private void SetDefaultData(string mode = "")
        {
            if (mode == "Add")
            {
               
            }
            else if (mode == "Edit")
            {
                
            }
        }
        private void SetButton(string ACTIVE_STEP = "")
        {
            SetDefaulButton(StandardButtonMode.Other);
            if (ACTIVE_STEP == "Assignment")
            {
                AddButton(StandButtonType.ButtonComfirmAjax, "btnSAVEASSIGN", Translation.MIS.MISS01P001.SAVEASSIGN, iconCssClass: FaIcons.FaCopy, url: Url.Action("SaveAssign"), isValidate: true);
            }
            else if (ACTIVE_STEP == "FilePacket")
            {
                AddButton(StandButtonType.ButtonComfirmAjax, "btnSAVEFILEPACKET", Translation.MIS.MISS01P001.SAVEASSIGN, iconCssClass: FaIcons.FaCopy, url: Url.Action("SaveFilePacket"), isValidate: true);
            }
        }
        //private List<DDLCenterModel> BindIssueType()
        //{
        //    return GetDDLCenter(DDLCenterKey.DD_MISS01P002_001, new VSMParameter(SessionHelper.SYS_COM_CODE.Trim()));
        //}
        //private List<DDLCenterModel> BindDefect()
        //{
        //    return GetDDLCenter(DDLCenterKey.DD_MISS01P002_002, new VSMParameter(SessionHelper.SYS_COM_CODE.Trim()));
        //}
        //private List<DDLCenterModel> BindPriority()
        //{
        //    return GetDDLCenter(DDLCenterKey.DD_MISS01P002_003, new VSMParameter(SessionHelper.SYS_COM_CODE.Trim()));
        //}
        //----------------------------------------------//
        private DTOResult SaveData(string mode, object model)
        {
            var da = new MISS01P002DA();
            //ในกรณีที่มีการ SaveLog ให้ Include SetStandardLog ด้วย
            //SetStandardLog(
            //   da.DTO,
            //   model,
            //   GetSaveLogConfig("dbo", "VSMS_COMPANY", "COM_CODE"));


            if (mode == StandardActionName.SaveCreate)
            {
                SetStandardField(model);
                da.DTO.Model = (MISS01P002Model)model;
                da.DTO.Execute.ExecuteType = MISS01P002ExecuteType.Insert;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;

                da.InsertNoEF(da.DTO);
            }
            else if (mode == StandardActionName.SaveModify)
            {
                SetStandardField(model);
                da.DTO.Execute.ExecuteType = MISS01P002ExecuteType.Update;
                da.DTO.Model = (MISS01P002Model)model;

                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.UpdateNoEF(da.DTO);
            }
            else if (mode == StandardActionName.Delete)
            {
                da.DTO.Models = (List<MISS01P002Model>)model;
                da.DTO.Model.COM_CODE = SessionHelper.SYS_COM_CODE;
                da.DeleteNoEF(da.DTO);
            }

            return da.DTO.Result;
        }
        #endregion
    }
}