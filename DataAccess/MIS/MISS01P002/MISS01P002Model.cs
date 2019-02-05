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