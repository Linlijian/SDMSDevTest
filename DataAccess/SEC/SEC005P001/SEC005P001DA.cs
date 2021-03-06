﻿using System.Data;
using System.Linq;
using UtilityLib;

namespace DataAccess.SEC
{
    public class SEC005P001DA : BaseDA
    {
        public SEC005P001DTO DTO { get; set; }

        public SEC005P001DA()
        {
            DTO = new SEC005P001DTO();
        }

        #region Select

        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (SEC005P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case SEC005P001ExecuteType.GetAll: return GetAll(dto);
                case SEC005P001ExecuteType.GetByID: return GetByID(dto);
                case SEC005P001ExecuteType.CHECK_DB_SERVER_NAME:
                    return CHECK_DB_SERVER_NAME(dto);
            }
            return dto;
        }
        private SEC005P001DTO GetAll(SEC005P001DTO dto)
        {
            dto.Models = _DBManger.VSMS_PROGRAM.Where(m => ((dto.Model.PRG_CODE == null || m.PRG_CODE.Contains(dto.Model.PRG_CODE)) &&
                         (dto.Model.PRG_NAME_TH == null || m.PRG_NAME_TH.Contains(dto.Model.PRG_NAME_TH)) &&
                         (dto.Model.PRG_NAME_EN == null || m.PRG_NAME_TH.Contains(dto.Model.PRG_NAME_EN)) &&
                         (dto.Model.PRG_TYPE == null || m.PRG_TYPE == dto.Model.PRG_TYPE) &&
                         (dto.Model.PRG_STATUS == null || m.PRG_STATUS == dto.Model.PRG_STATUS))).OrderBy(m => m.PRG_CODE)
                        .Select(m => new SEC005P001Model
                        {
                            COM_CODE = m.COM_CODE,
                            PRG_CODE = m.PRG_CODE,
                            PRG_NAME_TH = m.PRG_NAME_TH,
                            PRG_NAME_EN = m.PRG_NAME_EN,
                            PRG_URL = m.PRG_URL,
                            PRG_TYPE = m.PRG_TYPE == "A" ? "Common Master" : m.PRG_TYPE == "M" ? "Maintenance" : m.PRG_TYPE == "R" ? "Report" : m.PRG_TYPE == "S" ? "System" : m.PRG_TYPE == "T" ? "Transaction" : m.PRG_TYPE,
                            PRG_LEVEL = m.PRG_LEVEL,
                            PRG_STATUS = m.PRG_STATUS == "D" ? "Non-Active" : m.PRG_STATUS == "E" ? "Active" : m.PRG_STATUS,
                            CRET_BY = m.CRET_BY,
                            CRET_DATE = m.CRET_DATE,
                            MNT_BY = m.MNT_BY,
                            MNT_DATE = m.MNT_DATE,
                            PRG_IMG = m.PRG_IMG
                        }).ToList();

            return dto;
        }

        private SEC005P001DTO GetByID(SEC005P001DTO dto)
        {
            dto.Model = _DBManger.VSMS_PROGRAM
                .Where(m => m.COM_CODE == dto.Model.COM_CODE).Where(m => m.PRG_CODE == dto.Model.PRG_CODE)
                .FirstOrDefault().ToNewObject(new SEC005P001Model());

            return dto;
        }

        private SEC005P001DTO CHECK_DB_SERVER_NAME(SEC005P001DTO dto)
        {
            string strSQL = @"  SELECT  @@SERVERNAME SERVER_NAME ";

            var parameters = CreateParameter();
            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, CommandType.Text);

            if (result.Success(dto))
            {
                dto.Model = result.OutputDataSet.Tables[0].ToObject<SEC005P001Model>();
            }

            return dto;
        }
        #endregion

        #region Insert

        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (SEC005P001DTO)baseDTO;
            dto.Model.PRG_CODE = dto.Model.PRG_CODE.Trim();

            var model = dto.Model.ToNewObject(new VSMS_PROGRAM());
            _DBManger.VSMS_PROGRAM.Add(model);
            
            return dto;
        }

        #endregion

        #region Update

        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (SEC005P001DTO)baseDTO;

            var model = _DBManger.VSMS_PROGRAM.First(m => m.COM_CODE == dto.Model.COM_CODE && m.PRG_CODE == dto.Model.PRG_CODE);
            model.MergeObject(dto.Model);

            return dto;
        }

        #endregion

        #region Delete

        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (SEC005P001DTO)baseDTO;
            foreach (var item in dto.Models)
            {
                var COM_CODE = item.COM_CODE;
                var PRG_CODE = item.PRG_CODE;

                var items = _DBManger.VSMS_PROGRAM.Where(m => m.COM_CODE == COM_CODE && m.PRG_CODE == PRG_CODE);
                _DBManger.VSMS_PROGRAM.RemoveRange(items);
            }
            return dto;
        }

        #endregion
    }
}
