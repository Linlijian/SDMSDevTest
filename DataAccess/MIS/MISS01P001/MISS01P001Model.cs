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