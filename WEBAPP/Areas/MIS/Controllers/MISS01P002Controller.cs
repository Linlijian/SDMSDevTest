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
            localModel.ACTIVE_STEP = "5"; //if set 2 then block step3 end
            if (ACTIVE_STEP == "1")
            {
                view = "Status1Open";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Status1Open");

            }
            else if (ACTIVE_STEP == "2")
            {
                view = "Status2Onprocess";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Status2Onprocess");

            }
            else if (ACTIVE_STEP == "3")
            {
                view = "Status3Followup";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Status3Followup");

            }
            else if (ACTIVE_STEP == "4")
            {
                view = "Status4xxx";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Status4xxx");

            }
            else if (ACTIVE_STEP == "5")
            {
                view = "Status5xxx";
                SetButton(ACTIVE_STEP);
                SetDefaultData(ACTIVE_STEP);
                SetClientSideRuleSet("Status5xxx");

            }

            SetHeaderWizard(new WizardHelper.WizardHeaderConfig(
                ACTIVE_STEP,
                localModel.ACTIVE_STEP,
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P001.STEP_1, Url.Action("Index", new { ACTIVE_STEP = "1" }), iconCssClass: FaIcons.FaAreaChart),
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P001.STEP_2, Url.Action("Index", new { ACTIVE_STEP = "2" }), iconCssClass: FaIcons.FaFile),
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P001.STEP_3, Url.Action("Index", new { ACTIVE_STEP = "3" }), iconCssClass: FaIcons.FaFile),
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P001.STEP_4, Url.Action("Index", new { ACTIVE_STEP = "4" }), iconCssClass: FaIcons.FaFile),
                new WizardHelper.WizardHeader(Translation.MIS.MISS01P001.STEP_5, Url.Action("Index", new { ACTIVE_STEP = "5" }), iconCssClass: FaIcons.FaFile)));

            return View(view, localModel);
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