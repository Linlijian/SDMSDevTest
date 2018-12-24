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

        [Display(Name = "ISSUE_TYPE", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public string ISSUE_TYPE { get; set; }
        [Display(Name = "TYPE_RATE", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public string TYPE_RATE { get; set; }
        [Display(Name = "MAN_PLM_SA", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public decimal? MAN_PLM_SA { get; set; }
        [Display(Name = "MAN_PLM_QA", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public decimal? MAN_PLM_QA { get; set; }
        [Display(Name = "MAN_PLM_PRG", ResourceType = typeof(Translation.MST.MSTS02P001))]
        public decimal? MAN_PLM_PRG { get; set; }

    }
    public class MSTS02P001Validator : AbstractValidator<MSTS02P001Model>
    {
        public MSTS02P001Validator()
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
            //RuleFor(t => t.TYPE_DAY).NotEmpty();
        }
    }
}