using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MIS
{
    public class MISS01P001DA : BaseDA
    {
        public MISS01P001DTO DTO { get; set; }

        #region ====Property========
        public MISS01P001DA()
        {
            DTO = new MISS01P001DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P001ExecuteType.GetAll: return GetAll(dto);
                case MISS01P001ExecuteType.GetByID: return GetByID(dto);
                case MISS01P001ExecuteType.GetNo: return GetNo(dto);
                case MISS01P001ExecuteType.GetAllStatus: return GetAll(dto);
            }
            return dto;
        }
        private MISS01P001DTO GetNo(MISS01P001DTO dto)
        {
            string strSQL = @"	SELECT  COUNT(NO) A
                                FROM VSMS_ISSUE
                                WHERE COM_CODE = @COM_CODE";
            var parameters = CreateParameter();
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Model.NO = result.OutputDataSet.Tables[0].Rows[0][0].AsInt();
            }

            return dto;
        }
        private MISS01P001DTO GetAll(MISS01P001DTO dto)
        {
            string strSQL = @"	SELECT *
	                            FROM VSMS_ISSUE
	                            WHERE (1=1) ";
            var parameters = CreateParameter();            

            if (!dto.Model.APP_CODE.IsNullOrEmpty()) //checked
            {
                strSQL += " AND COM_CODE = @APP_CODE";
                parameters.AddParameter("APP_CODE", dto.Model.APP_CODE);
            }
            if (!dto.Model.NO.IsNullOrEmpty())
            {
                strSQL += " AND NO = @NO";
                parameters.AddParameter("NO", dto.Model.NO);
            }
            if (!dto.Model.MODULE.IsNullOrEmpty())
            {
                strSQL += " AND MODULE = @MODULE";
                parameters.AddParameter("MODULE", dto.Model.MODULE);
            }
            if (!dto.Model.STATUS.IsNullOrEmpty())
            {
                strSQL += " AND STATUS = @STATUS";
                parameters.AddParameter("STATUS", dto.Model.STATUS);
            }
            if (!dto.Model.RESPONSE_BY.IsNullOrEmpty())
            {
                strSQL += " AND RESPONSE_BY = @RESPONSE_BY";
                parameters.AddParameter("RESPONSE_BY", dto.Model.RESPONSE_BY);
            }
            if (!dto.Model.PRIORITY.IsNullOrEmpty())
            {
                strSQL += " AND PRIORITY = @PRIORITY";
                parameters.AddParameter("PRIORITY", dto.Model.PRIORITY);
            }

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS01P001Model>();
            }

            return dto;
        }
        private MISS01P001DTO GetByID(MISS01P001DTO dto)
        {
            string strSQL = @"	SELECT *
	                            FROM VSMS_ISSUE
	                            WHERE (1=1)
	                            AND COM_CODE = @COM_CODE
                                AND NO = @NO";

            var parameters = CreateParameter();
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("NO", dto.Model.NO);
            
            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Model = result.OutputDataSet.Tables[0].ToObject<MISS01P001Model>();
            }
            return dto;
        }
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P001ExecuteType.Insert: return Insert(dto);
            }
            return dto;
        }
        private MISS01P001DTO Insert(MISS01P001DTO dto)
        {
            var parameters = CreateParameter();

            parameters.AddParameter("error_code", null, ParameterDirection.Output);
            parameters.AddParameter("COM_CODE", dto.Model.APP_CODE); //checked
            parameters.AddParameter("NO", dto.Model.NO);
            parameters.AddParameter("ISSUE_DATE", dto.Model.ISSUE_DATE);
            parameters.AddParameter("ISSUE_DATE_PERIOD", dto.Model.ISSUE_DATE_PERIOD);
            parameters.AddParameter("MODULE", dto.Model.MODULE);
            parameters.AddParameter("DETAIL", dto.Model.DETAIL);
            parameters.AddParameter("ROOT_CAUSE", dto.Model.ROOT_CAUSE);
            parameters.AddParameter("SOLUTION", dto.Model.SOLUTION);
            parameters.AddParameter("EFFECTS", dto.Model.EFFECTS);
            parameters.AddParameter("ISSUE_BY", dto.Model.ISSUE_BY);
            parameters.AddParameter("RESPONSE_BY", dto.Model.RESPONSE_BY);
            parameters.AddParameter("TARGET_DATE", dto.Model.TARGET_DATE);
            parameters.AddParameter("RESPONSE_DATE", dto.Model.RESPONSE_DATE);
            parameters.AddParameter("RESPONSE_TARGET", dto.Model.RESPONSE_TARGET);
            parameters.AddParameter("RESOLUTION_TARGET", dto.Model.RESOLUTION_TARGET);
            parameters.AddParameter("ESSR_NO", dto.Model.ESSR_NO);
            parameters.AddParameter("CLOSE_DATE", dto.Model.CLOSE_DATE);
            parameters.AddParameter("ISSUE_TYPE", dto.Model.ISSUE_TYPE);
            parameters.AddParameter("DEFECT", dto.Model.DEFECT);
            parameters.AddParameter("PRIORITY", dto.Model.PRIORITY);
            parameters.AddParameter("REMARK", dto.Model.REMARK);
            parameters.AddParameter("MAN_PLM_SA", dto.Model.MAN_PLM_SA);
            parameters.AddParameter("MAN_PLM_QA", dto.Model.MAN_PLM_QA);
            parameters.AddParameter("MAN_PLM_PRG", dto.Model.MAN_PLM_PRG);
            parameters.AddParameter("MAN_PLM_PL", dto.Model.MAN_PLM_PL);
            parameters.AddParameter("MAN_PLM_DBA", dto.Model.MAN_PLM_DBA);
            parameters.AddParameter("ISSUE_IMG", dto.Model.ISSUE_IMG);
            parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
            parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);

            var result = _DBMangerNoEF.ExecuteDataSet("[bond].[SP_VSMS_ISSUE_001]", parameters, CommandType.StoredProcedure);

            if (!result.Status)
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = result.ErrorMessage;
            }
            else
            {
                if (result.OutputData["error_code"].ToString().Trim() != "0")
                {
                    dto.Result.IsResult = false;
                    dto.Result.ResultMsg = result.OutputData["error_code"].ToString().Trim();
                }
            }

            return dto;
        }
        #endregion

        #region ====Update==========
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P001ExecuteType.Update: return Update(dto);
            }
            return dto;
        }
        private MISS01P001DTO Update(MISS01P001DTO dto)
        {
            var parameters = CreateParameter();

            parameters.AddParameter("error_code", null, ParameterDirection.Output);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("NO", dto.Model.NO);
            parameters.AddParameter("ISSUE_DATE", dto.Model.ISSUE_DATE);
            parameters.AddParameter("ISSUE_DATE_PERIOD", dto.Model.ISSUE_DATE_PERIOD);
            parameters.AddParameter("MODULE", dto.Model.MODULE);
            parameters.AddParameter("DETAIL", dto.Model.DETAIL);
            parameters.AddParameter("ROOT_CAUSE", dto.Model.ROOT_CAUSE);
            parameters.AddParameter("SOLUTION", dto.Model.SOLUTION);
            parameters.AddParameter("EFFECTS", dto.Model.EFFECTS);
            parameters.AddParameter("ISSUE_BY", dto.Model.ISSUE_BY);
            parameters.AddParameter("RESPONSE_BY", dto.Model.RESPONSE_BY);
            parameters.AddParameter("RESPONSE_DATE", dto.Model.RESPONSE_DATE);
            parameters.AddParameter("RESPONSE_TARGET", dto.Model.RESPONSE_TARGET);
            parameters.AddParameter("RESOLUTION_TARGET", dto.Model.RESOLUTION_TARGET);
            parameters.AddParameter("ESSR_NO", dto.Model.ESSR_NO);
            parameters.AddParameter("ISSUE_TYPE", dto.Model.ISSUE_TYPE);
            parameters.AddParameter("DEFECT", dto.Model.DEFECT);
            parameters.AddParameter("PRIORITY", dto.Model.PRIORITY);
            parameters.AddParameter("REMARK", dto.Model.REMARK);
            parameters.AddParameter("MAN_PLM_SA", dto.Model.MAN_PLM_SA);
            parameters.AddParameter("MAN_PLM_QA", dto.Model.MAN_PLM_QA);
            parameters.AddParameter("MAN_PLM_PRG", dto.Model.MAN_PLM_PRG);
            parameters.AddParameter("MAN_PLM_PL", dto.Model.MAN_PLM_PL);
            parameters.AddParameter("MAN_PLM_DBA", dto.Model.MAN_PLM_DBA);
            parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
            parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);

            var result = _DBMangerNoEF.ExecuteDataSet("[bond].[SP_VSMS_ISSUE_004]", parameters, CommandType.StoredProcedure);

            if (!result.Status)
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = result.ErrorMessage;
            }
            else
            {
                if (result.OutputData["error_code"].ToString().Trim() != "0")
                {
                    dto.Result.IsResult = false;
                    dto.Result.ResultMsg = result.OutputData["error_code"].ToString().Trim();
                }
            }

            return dto;
        }
        #endregion
    }
}