using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.MST
{
    [Validator(typeof(MSTS02P001Validator))]
    [Serializable]
    public class MSTS02P001Model : StandardModel
    {
        [Display(Name = "YEAR", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public string YEAR { get; set; }
        [Display(Name = "IS_USE", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public string IS_USE { get; set; }
        [Display(Name = "MANDAY_VAL", ResourceType = typeof(Translation.MST.MSTS02P001))]
        //public string MANDAY_VAL { get; set; }
        public decimal? MANDAY_VAL { get; set; }

        [Display(Name = "COM_CODE", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public string COM_CODE { get; set; }
        public IEnumerable<DDLCenterModel> COM_CODE_MODEL { get; set; }

    }
    public class MSTS02P001Validator : AbstractValidator<MSTS02P001Model>
    {
        public MSTS02P001Validator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(m => m.COM_CODE).Store("CD_MSTS02P001_001", m => m.YEAR).NotEmpty();
                RuleFor(m => m.YEAR).Store("CD_MSTS02P001_001", m => m.COM_CODE).NotEmpty();
                RuleFor(m => m.COM_CODE).Store("CD_MSTS02P001_001", m => m.COM_CODE); 
                Valid(); 
            });
            RuleSet("Edit", () =>
            {
                RuleFor(m => m.COM_CODE).Store("CD_MSTS02P001_001", m => m.COM_CODE); 
                Valid();
            });
        }

        private void Valid()
        {
            RuleFor(t => t.MANDAY_VAL).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(Convert.ToDecimal(99.9)).WithMessage(Translation.CenterLang.Validate.OneNumber2Digit1);
        }
    }
}