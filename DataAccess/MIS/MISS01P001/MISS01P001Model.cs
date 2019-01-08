using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.MIS
{
    [Validator(typeof(MISS01P001Validator))]
    [Serializable]
    public class MISS01P001Model : StandardModel
    {
        [Display(Name = "MODULE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string MODULE { get; set; }
        [Display(Name = "DETAIL", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string DETAIL { get; set; }
        [Display(Name = "ROOT_CAUSE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string ROOT_CAUSE { get; set; }
        [Display(Name = "SOLUTION", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string SOLUTION { get; set; }
        [Display(Name = "EFFECTS", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string EFFECTS { get; set; }
        [Display(Name = "ISSUE_BY", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string ISSUE_BY { get; set; }
        [Display(Name = "RESPONSE_BY", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string RESPONSE_BY { get; set; }
        [Display(Name = "FILE_ID", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string FILE_ID { get; set; }
        [Display(Name = "STATUS", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string STATUS { get; set; }
        [Display(Name = "PROGRAM_NAME", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string PROGRAM_NAME { get; set; }
        [Display(Name = "FILE_NAME", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string ISSUE_IMG { get; set; }
        [Display(Name = "PRIORITY", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string PRIORITY { get; set; }
        [Display(Name = "COMMENT", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string REMARK { get; set; }
        [Display(Name = "RESOLUTION_TARGET", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string RESOLUTION_TARGET { get; set; }

        [Display(Name = "ISSUE_TYPE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string ISSUE_TYPE { get; set; }
        public IEnumerable<DDLCenterModel> ISSUE_TYPE_MODEL { get; set; }
        [Display(Name = "DEFECT", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public string DEFECT { get; set; }
        public IEnumerable<DDLCenterModel> DEFECT_MODEL { get; set; }

        public string ACTIVE_STEP { get; set; }

        [Display(Name = "NO", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? NO { get; set; }
        [Display(Name = "ESSR_NO", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? ESSR_NO { get; set; }
        [Display(Name = "MAN_PLM_SA", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? MAN_PLM_SA { get; set; }
        [Display(Name = "MAN_PLM_QA", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? MAN_PLM_QA { get; set; }
        [Display(Name = "MAN_PLM_PRG", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? MAN_PLM_PRG { get; set; }
        [Display(Name = "MAN_PLM_PL", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? MAN_PLM_PL { get; set; }
        [Display(Name = "MAN_PLM_DBA", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public decimal? MAN_PLM_DBA { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ISSUE_DATE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? ISSUE_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ISSUE_DATE_PERIOD", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? ISSUE_DATE_PERIOD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "TARGET_DATE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? TARGET_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RESPONSE_DATE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? RESPONSE_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CLOSE_DATE", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? CLOSE_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DEPLOY_QA", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? DEPLOY_QA { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DEPLOY_PD", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? DEPLOY_PD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ACTUAL_RESTIME", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? ACTUAL_RESTIME { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "RESPONSE_TARGET", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? RESPONSE_TARGET { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ACTUAL_RESTIME_QA", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? ACTUAL_RESTIME_QA { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ACTUAL_RESTIME_PD", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? ACTUAL_RESTIME_PD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ACTUAL_RESTIME_CL", ResourceType = typeof(Translation.MIS.MISS01P001))]
        public DateTime? ACTUAL_RESTIME_CL { get; set; }
    }
    public class MISS01P001Validator : AbstractValidator<MISS01P001Model>
    {
        public MISS01P001Validator()
        {
            RuleSet("Add", () =>
            {
                Valid();
            });
            RuleSet("Edit", () =>
            {
                Valid();
            });
            RuleSet("Upload", () =>
            {
                
            });
        }

        private void Valid()
        {
            //RuleFor(t => t.TYPE_DAY).NotEmpty();
        }
    }
}