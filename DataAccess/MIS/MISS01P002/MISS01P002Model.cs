using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataAccess.MIS
{
    [Validator(typeof(MISS01P002Validator))]
    [Serializable]
    public class MISS01P002Model : StandardModel
    {
        [Display(Name = "NO", ResourceType = typeof(Translation.MIS.MISS01P002))]
        public decimal? NO { get; set; }

        [Display(Name = "RESPONSE_BY", ResourceType = typeof(Translation.MIS.MISS01P002))]
        public string RESPONSE_BY { get; set; }
        [Display(Name = "ASSIGN_USER", ResourceType = typeof(Translation.MIS.MISS01P002))]
        public string ASSIGN_USER { get; set; }
        [Display(Name = "FILE_ID", ResourceType = typeof(Translation.MIS.MISS01P002))]
        public string FILE_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "ISSUE_DATE", ResourceType = typeof(Translation.MIS.MISS01P002))]
        public Nullable<System.DateTime> ISSUE_DATE { get; set; }

        [Display(Name = "STATUS", ResourceType = typeof(Translation.MIS.MISS01P002))]
        public string STATUS { get; set; }
        public IEnumerable<DDLCenterModel> STATUS_MODEL { get; set; }
    }
    public class MISS01P002Validator : AbstractValidator<MISS01P002Model>
    {
        public MISS01P002Validator()
        {
            RuleSet("Add", () =>
            {
                Valid();
            });
            RuleSet("Edit", () =>
            {
                Valid();
            });
            RuleSet("FilePacket", () =>
            {
              
            });
            RuleSet("Assignment", () =>
            {
               
            });
        }

        private void Valid()
        {

        }
    }
}