using System.Data;
using System.Linq;
using UtilityLib;

namespace DataAccess.SEC
{
    public class SECS01P001DA : BaseDA
    {
        public SECS01P001DTO DTO { get; set; }

        #region ====Property========
        public SECS01P001DA()
        {
            DTO = new SECS01P001DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (SECS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case SECS01P001ExecuteType.GetAll: return GetAll(dto);
                case SECS01P001ExecuteType.GetByID: return GetByID(dto);
                case SECS01P001ExecuteType.GetComLicenseID: return GetComLicenseID(dto);
                case SECS01P001ExecuteType.GetDetailByID: return GetDetailByID(dto);
            }
            return dto;
        }

        private SECS01P001DTO GetAll(SECS01P001DTO dto)
        {
            dto.Models = _DBManger.VSMS_COMPANY
                .Where((m => ((dto.Model.COM_CODE == null || dto.Model.COM_CODE == string.Empty) || m.COM_CODE.Contains(dto.Model.COM_CODE))
                && ((dto.Model.COM_BRANCH == null || dto.Model.COM_BRANCH == string.Empty) || m.COM_BRANCH.Contains(dto.Model.COM_BRANCH))
                && ((dto.Model.COM_NAME_E == null || dto.Model.COM_NAME_E == string.Empty) || m.COM_NAME_E.Contains(dto.Model.COM_NAME_E))
                && ((dto.Model.COM_FAC_NAME_E == null || dto.Model.COM_FAC_NAME_E == string.Empty) || m.COM_FAC_NAME_E.Contains(dto.Model.COM_FAC_NAME_E))
                && ((dto.Model.COM_BRANCH_E == null || dto.Model.COM_BRANCH_E == string.Empty) || m.COM_BRANCH_E.Contains(dto.Model.COM_BRANCH_E))
                ))
                //.OrderBy(m => new { m.COM_CODE, m.COM_CODE })
                .Select(m => new SECS01P001Model
                {
                    COM_CODE = m.COM_CODE,
                    COM_BRANCH = m.COM_BRANCH,
                    COM_NAME_E = m.COM_NAME_E,
                    COM_NAME_T = m.COM_NAME_T,
                    COM_BRANCH_E = m.COM_BRANCH_E
                }).ToList();

            return dto;
        }

        private SECS01P001DTO GetDetailByID(SECS01P001DTO dto)
        {
            dto.Model.Details = _DBManger.VSMS_MOBULE
                .Where((m => ((m.COM_CODE == dto.Model.COM_CODE))
                ))
                .Select(m => new SECS01P001DetailPModel
                {
                    COM_CODE = m.COM_CODE,
                    MODULE = m.MODULE,
                    USER_ID = m.USER_ID
                }).ToList();

            return dto;
        }

        private SECS01P001DTO GetByID(SECS01P001DTO dto)
        {
            dto.Model = _DBManger.VSMS_COMPANY
                .Where(m => m.COM_CODE == dto.Model.COM_CODE && m.COM_BRANCH == dto.Model.COM_BRANCH)
                .FirstOrDefault().ToNewObject(new SECS01P001Model());
            dto.Model.COM_CODE = dto.Model.COM_CODE;
            dto.Model.COM_BRANCH = dto.Model.COM_BRANCH;
            return dto;
        }

        private SECS01P001DTO GetComLicenseID(SECS01P001DTO dto)
        {
            dto.Model = _DBManger.VSMS_COMPANY
                .Where(m => m.COM_CODE == dto.Model.COM_CODE)
                .Select(m => new SECS01P001Model
                {
                    COM_LICENSE_ID = m.COM_LICENSE_ID
                })
                .FirstOrDefault().ToNewObject(new SECS01P001Model());

            return dto;
        }
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (SECS01P001DTO)baseDTO;
            if(dto.Model.COM_USE_LANGUAGE != null)
            {
                dto.Model.COM_USE_LANGUAGE = dto.Model.COM_USE_LANGUAGE.Trim();
            }
            

            //var COM_CODE = _DBManger.VSMS_COMPANY.Max(m => m.COM_CODE).AsString() + 1;
            //dto.Model.COM_CODE = COM_CODE;

            var model = dto.Model.ToNewObject(new VSMS_COMPANY());
            var COM_CODE = model.COM_CODE;
            var COM_BRANCH = model.COM_BRANCH;

            model.COM_CODE = COM_CODE;
            model.COM_BRANCH = COM_BRANCH;
            model.CRET_BY = model.CRET_BY.Trim();
            model.MNT_BY = model.MNT_BY.Trim();
            _DBManger.VSMS_COMPANY.Add(model);

            InsertDetail(dto);

            return dto;
        }

        private SECS01P001DTO InsertDetail(SECS01P001DTO dto)
        {
            if (dto.Model.Details.Count() > 0)
            {
                foreach (var item in dto.Model.Details)
                {
                    var m = item.ToNewObject(new VSMS_MOBULE());
                    m.CRET_BY = dto.Model.CRET_BY;
                    m.CRET_DATE = dto.Model.CRET_DATE;
                    m.COM_CODE = dto.Model.COM_CODE;

                    _DBManger.VSMS_MOBULE.Add(m);
                }
            }

            return dto;
        }
        #endregion

        #region ====Update==========
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (SECS01P001DTO)baseDTO;
            if (dto.Model.COM_USE_LANGUAGE != null)
            {
                dto.Model.COM_USE_LANGUAGE = dto.Model.COM_USE_LANGUAGE.Trim();
            }
            var COM_CODE = dto.Model.COM_CODE;  //เอา  COM_CODE มาหาในเทเบิล เพื่อ อัพเดท
            var COM_BRANCH = dto.Model.COM_BRANCH;
            var model = _DBManger.VSMS_COMPANY.First(m => m.COM_CODE == COM_CODE && m.COM_BRANCH == COM_BRANCH);
            model.MergeObject(dto.Model);

            UpdateDetail(dto);

            return dto;
        }

        private SECS01P001DTO UpdateDetail(SECS01P001DTO dto)
        {
            if (dto.Model.Details.Count() > 0)
            {
                var items = _DBManger.VSMS_MOBULE.Where(m => m.COM_CODE == dto.Model.COM_CODE);
                _DBManger.VSMS_MOBULE.RemoveRange(items);

                InsertDetail(dto);
            }

            return dto;
        }
        #endregion

        #region ====Delete==========
        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (SECS01P001DTO)baseDTO;
            foreach (var item in dto.Models)
            {

                var items = _DBManger.VSMS_COMPANY.Where(m => m.COM_CODE == item.COM_CODE && m.COM_BRANCH == item.COM_BRANCH);
                _DBManger.VSMS_COMPANY.RemoveRange(items);
            }
            return dto;
        }
        #endregion
    }
}