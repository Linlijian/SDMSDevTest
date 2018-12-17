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
            //RuleFor(t => t.TYPE_DAY).NotEmpty();
        }
    }
}