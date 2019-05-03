using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataAccess.MIS
{
    [Validator(typeof(MISS01P003Validator))]
    [Serializable]
    public class MISS01P003Model : StandardModel
    {
        public IEnumerable<DDLCenterModel> APP_CODE_MODEL { get; set; }
        public IEnumerable<DDLCenterModel> STATUS_MODEL { get; set; }

        [Display(Name = "STATUS", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string STATUS { get; set; }
        [Display(Name = "CANCEL", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string CANCEL { get; set; }
        [Display(Name = "AGREED", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string AGREED { get; set; }
        [Display(Name = "STATUS", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string ASSIGN_STATUS { get; set; }
        [Display(Name = "RESPONSE_BY", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string RESPONSE_BY { get; set; }
        [Display(Name = "COM_NAME_E", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string COM_NAME_E { get; set; }
        [Display(Name = "ISE_NO", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? ISE_NO { get; set; }
        [Display(Name = "ISSUE_BY", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string ISSUE_BY { get; set; }
        [Display(Name = "ISSUE_DATE_PERIOD", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string ISSUE_DATE_PERIOD { get; set; }
        [Display(Name = "ISSUE_TYPE", ResourceType = typeof(Translation.MIS.MISS01P003))]  //*---
        public string ISSUE_TYPE { get; set; }
        [Display(Name = "MODULE", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string MODULE { get; set; }
        [Display(Name = "MENU", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string MENU { get; set; }
        [Display(Name = "PRG_NAME", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string PRG_NAME { get; set; }
        [Display(Name = "ESSR_NO", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? ESSR_NO { get; set; }
        [Display(Name = "ROOT_CAUSE", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string ROOT_CAUSE { get; set; }
        [Display(Name = "SOLUTION", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string SOLUTION { get; set; }
        [Display(Name = "EFFECTS", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string EFFECTS { get; set; }
        [Display(Name = "DETAIL", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string DETAIL { get; set; }
        [Display(Name = "RESPONSE_TARGET", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string RESPONSE_TARGET { get; set; }
        [Display(Name = "MAN_PLM_SA", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? MAN_PLM_SA { get; set; }
        [Display(Name = "MAN_PLM_QA", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? MAN_PLM_QA { get; set; }
        [Display(Name = "MAN_PLM_PRG", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? MAN_PLM_PRG { get; set; }
        [Display(Name = "MAN_PLM_PL", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? MAN_PLM_PL { get; set; }
        [Display(Name = "MAN_PLM_DBA", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? MAN_PLM_DBA { get; set; }
        [Display(Name = "COMMENT", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string REMARK { get; set; }
        [Display(Name = "RESOLUTION_TARGET", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string RESOLUTION_TARGET { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLOSE_DATE", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> CLOSE_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "DEPLOY_QA", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> DEPLOY_QA { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "DEPLOY_PD", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> DEPLOY_PD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "ISSUE_DATE", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> ISSUE_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "RESPONSE_DATE", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> RESPONSE_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "TARGET_DATE", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> TARGET_DATE { get; set; }
        [Display(Name = "DEFECT", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public string DEFECT { get; set; }
        public IEnumerable<DDLCenterModel> DEFECT_MODEL { get; set; }




        //[Display(Name = "NO", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public decimal? NO { get; set; }

        public string STR_TARGET_DATE { get; set; }
        public string STR_RESPONSE_DATE { get; set; }
        public string STR_ISSUE_DATE { get; set; }
        public string STR_RDEPLOY_PD { get; set; }
        public string STR_DEPLOY_QA { get; set; }
        public string STR_CLOSE_DATE { get; set; }
        public string ISE_KEY { get; set; }
        public string FALG { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "ISE_DATE_OPENING", ResourceType = typeof(Translation.MIS.MISS01P003))]
        public Nullable<System.DateTime> ISE_DATE_OPENING { get; set; }

        [Display(Name = "PRIORITY", ResourceType = typeof(Translation.MIS.MISS01P003))] //*--
        public string PRIORITY { get; set; }
        public IEnumerable<DDLCenterModel> PRIORITY_MODEL { get; set; }



    }
    public class MISS01P003Validator : AbstractValidator<MISS01P003Model>
    {
        public MISS01P003Validator()
        {
            RuleSet("Add", () =>
            {
                Valid();
            });
            
        }

        private void Valid()
        {

        }
    }
}