using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.SEC
{
    [Validator(typeof(SECS02P002Validator))]
    [Serializable]
    public class SECS02P002Model : StandardModel
    {
        [Display(Name = "USER_ID", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string USER_ID { get; set; }

        [Display(Name = "USER_FNAME_TH", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string USER_FNAME_TH { get; set; }

        [Display(Name = "USER_LNAME_TH", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string USER_LNAME_TH { get; set; }

        [Display(Name = "USER_FNAME_EN", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string USER_FNAME_EN { get; set; }

        [Display(Name = "USER_LNAME_EN", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string USER_LNAME_EN { get; set; }

        [Display(Name = "TITLE_ID", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public decimal? TITLE_ID { get; set; }
        [Display(Name = "TITLE_NAME_TH", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string TITLE_NAME_TH { get; set; }
        public IEnumerable<DDLCenterModel> TITLE_ID_MODEL { get; set; }       

        public string USG_NAME_TH { get; set; }

        [Display(Name = "USG_ID", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public decimal? USG_ID { get; set; }
        public IEnumerable<DDLCenterModel> USG_ID_MODEL { get; set; }

        public string USG_LEVEL { get; set; }

        [Display(Name = "USER_STATUS", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string USER_STATUS { get; set; }
        public IEnumerable<DDLCenterModel> USER_STATUS_MODEL { get; set; }

        [Display(Name = "IS_DISABLED", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string IS_DISABLED { get; set; }
        public IEnumerable<DDLCenterModel> IS_DISABLED_MODEL { get; set; }

        public string USER_PWD { get; set; }

        [Display(Name = "TELEPHONE", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string TELEPHONE { get; set; }

        [Display(Name = "EMAIL", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string EMAIL { get; set; }

        public DateTime? LAST_LOGIN_DATE { get; set; }

        public int IS_DUP { get; set; }

        [Display(Name = "APP_CODE", ResourceType = typeof(Translation.SEC.SECS02P002))]
        public string APP_CODE { get; set; }
        public IEnumerable<DDLCenterModel> APP_CODE_MODEL { get; set; }
    }
    
    public class SECS02P002Validator : AbstractValidator<SECS02P002Model>
    {
        public SECS02P002Validator()
        {
            RuleSet("Add", () =>
            {
                Valid();
                //RuleFor(m => m.USER_ID).NotEmpty();
            });

            RuleSet("Edit", () =>
            {
                Valid();
            });
        }

        private void Valid()
        {
            RuleFor(m => m.USER_FNAME_TH).NotEmpty();
            RuleFor(m => m.USER_FNAME_EN).NotEmpty();
            RuleFor(m => m.TITLE_ID).NotEmpty();
            RuleFor(m => m.USER_STATUS).NotEmpty();
        }
    }
}