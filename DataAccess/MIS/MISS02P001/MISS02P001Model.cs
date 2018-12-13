using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.MIS
{
    [Validator(typeof(MISS02P001Validator))]
    [Serializable]
    public class MISS02P001Model : StandardModel
    {
        [Display(Name = "YEAR", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string YEAR { get; set; }
        [Display(Name = "MONTH", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string MONTH { get; set; }
        [Display(Name = "DAY", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string DAY { get; set; }

        [Display(Name = "DEPLOYMENT_DATE", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string DEPLOYMENT_DATE { get; set; }

        [Display(Name = "TYPE_DAY", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string TYPE_DAY { get; set; }
        public IEnumerable<DDLCenterModel> TYPE_DAY_MODEL { get; set; }

        public string ALL_SATURDAY_W { get; set; } //W
        public string ALL_HOLIDAY { get; set; } //H
        public string ALL_SPP { get; set; } //S
        public string ALL_DEPLOYMENT_IT { get; set; } //I
        public string ALL_DEPLOYMENT_DATE { get; set; } //D
        public string CLIENT_ID { get; set; }
        public string YYYY { get; set; }
        public string MM { get; set; }
        public string DD { get; set; }
        public string FILE_EXCEL { get; set; }

        public string ERROR_CODE { get; set; }
        public string ERROR_MSG { get; set; }

        public List<MISS02P001DetailPModel> Details { get; set; }

        public System.Data.DataSet ds { get; set; }
    }

    public class MISS02P001DetailPModel
    {
        [Display(Name = "TYPE_DAY", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string TYPE_DAY { get; set; }
        public IEnumerable<DDLCenterModel> TYPE_DAY_MODEL { get; set; }

        [Display(Name = "YEAR", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string YEAR { get; set; }
        [Display(Name = "MONTH", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string MONTH { get; set; }
        [Display(Name = "DAY", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string DAY { get; set; }

        [Display(Name = "DEPLOYMENT_DATE", ResourceType = typeof(Translation.MIS.MISS02P001))]
        public string DEPLOYMENT_DATE { get; set; }
    }
    public class MISS02P001Validator : AbstractValidator<MISS02P001Model>
    {
        public MISS02P001Validator()
        {

        }

        private void Valid()
        {

        }
    }
}