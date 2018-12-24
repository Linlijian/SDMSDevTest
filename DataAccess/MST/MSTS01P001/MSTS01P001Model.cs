using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.MST
{
    [Validator(typeof(MSTS01P001Validator))]
    [Serializable]
    public class MSTS01P001Model : StandardModel
    {

        [Display(Name = "ISSUE_TYPE", ResourceType = typeof(Translation.MST.MSTS01P001))]
        public string ISSUE_TYPE { get; set; }
        public IEnumerable<DDLCenterModel> ISSUE_TYPE_MODEL { get; set; }
        [Display(Name = "TYPE_RATE", ResourceType = typeof(Translation.MST.MSTS01P001))]
        public string TYPE_RATE { get; set; }
        public IEnumerable<DDLCenterModel> TYPE_RATE_MODEL { get; set; }

        [Display(Name = "MAN_PLM_SA", ResourceType = typeof(Translation.MST.MSTS01P001))]
        public decimal? MAN_PLM_SA { get; set; }
        [Display(Name = "MAN_PLM_QA", ResourceType = typeof(Translation.MST.MSTS01P001))]
        public decimal? MAN_PLM_QA { get; set; }
        [Display(Name = "MAN_PLM_PRG", ResourceType = typeof(Translation.MST.MSTS01P001))]
        public decimal? MAN_PLM_PRG { get; set; }

    }
    public class MSTS01P001Validator : AbstractValidator<MSTS01P001Model>
    {
        public MSTS01P001Validator()
        {
            RuleSet("Add", () =>
            {
                Valid();
            });
            RuleSet("Edit", () =>
            {
                Valid();
            });
        }

        private void Valid()
        {
            RuleFor(t => t.ISSUE_TYPE).NotEmpty();
            RuleFor(t => t.TYPE_RATE).NotEmpty();
            RuleFor(t => t.MAN_PLM_SA).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(Convert.ToDecimal(99.9)).WithMessage(Translation.CenterLang.Validate.OneNumber2Digit1);
            RuleFor(t => t.MAN_PLM_QA).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(Convert.ToDecimal(99.9)).WithMessage(Translation.CenterLang.Validate.OneNumber2Digit1);
            RuleFor(t => t.MAN_PLM_PRG).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(Convert.ToDecimal(99.9)).WithMessage(Translation.CenterLang.Validate.OneNumber2Digit1);
        }
    }
}