using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MIS
{
    public class MISS01P003DA : BaseDA
    {
        public MISS01P003DTO DTO { get; set; }

        #region ====Property========
        public MISS01P003DA()
        {
            DTO = new MISS01P003DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MISS01P003DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P003ExecuteType.GetAll: return GetAll(dto);
                case MISS01P003ExecuteType.GetDoneTask: return GetDoneTask(dto);
                case MISS01P003ExecuteType.GetDoTask: return GetDoTask(dto);
                case MISS01P003ExecuteType.GetNewTask: return GetNewTask(dto);
                case MISS01P003ExecuteType.SolutionResult: return SolutionResult(dto);
            }
            return dto;
        }
        private MISS01P003DTO SolutionResult(MISS01P003DTO dto)
        {
            string strSQL = @"	SELECT SOLUTION , NO as ISE_NO
                                FROM VSMS_ISSUE
                                WHERE COM_CODE = @COM_CODE
                                AND NO = @NO";
            var parameters = CreateParameter();
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("NO", dto.Model.ISE_NO); //cheked

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Model = result.OutputDataSet.Tables[0].ToObject<MISS01P003Model>();
            }

            return dto;
        }
        private MISS01P003DTO GetAll(MISS01P003DTO dto)
        {
            string strSQL = string.Empty;
            strSQL = @"	SELECT t.COM_CODE
	                        ,t.ISE_NO
	                        ,t.RESPONSE_BY
	                        ,t.ISE_DATE_OPENING
	                        ,t.ASSIGN_STATUS
	                        ,tt.COM_NAME_E
                            ,t.ISE_KEY
                        FROM VSMS_COMPANY tt
                        INNER JOIN (
	                        SELECT t.COM_CODE
		                        ,t.ISE_NO
		                        ,tt.RESPONSE_BY
		                        ,t.ISE_DATE_OPENING
		                        ,t.ASSIGN_STATUS
                                ,t.ISE_KEY
	                        FROM VSMS_ISSTATOPSS t
	                        INNER JOIN VSMS_ISSUE tt ON t.COM_CODE = tt.COM_CODE
		                        AND t.ISE_NO = tt.no
	                        WHERE t.USER_ID = @USER_ID
	                        ) t ON t.COM_CODE = tt.COM_CODE
                        --WHERE t.RESPONSE_BY = @USER_ID
                        ";


            var parameters = CreateParameter();
            parameters.AddParameter("USER_ID", dto.Model.CRET_BY);

            if (!dto.Model.APP_CODE.IsNullOrEmpty())
            {
                strSQL += " AND t.COM_CODE = @APP_CODE";
                parameters.AddParameter("APP_CODE", dto.Model.APP_CODE);
            }
            if (!dto.Model.STATUS.IsNullOrEmpty())
            {
                strSQL += " AND t.ASSIGN_STATUS = @STATUS";
                parameters.AddParameter("STATUS", dto.Model.STATUS);
            }

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS01P003Model>();
            }

            return dto;
        }
        private MISS01P003DTO GetNewTask(MISS01P003DTO dto)
        {
            string strSQL = string.Empty;
            strSQL = @"	SELECT t.COM_CODE
	                        ,t.ISE_NO
	                        ,t.RESPONSE_BY
	                        ,t.ISE_DATE_OPENING
	                        ,t.ASSIGN_STATUS
	                        ,tt.COM_NAME_E
                            ,t.ISE_KEY
                        FROM VSMS_COMPANY tt
                        INNER JOIN (
	                        SELECT t.COM_CODE
		                        ,t.ISE_NO
		                        ,tt.RESPONSE_BY
		                        ,t.ISE_DATE_OPENING
		                        ,t.ASSIGN_STATUS
                                ,t.ISE_KEY
	                        FROM VSMS_ISSTATOPSS t
	                        INNER JOIN VSMS_ISSUE tt ON t.COM_CODE = tt.COM_CODE
		                        AND t.ISE_NO = tt.no
	                        WHERE t.USER_ID = @USER_ID
	                        ) t ON t.COM_CODE = tt.COM_CODE
                        AND t.ASSIGN_STATUS in ('W')
                        ";


            var parameters = CreateParameter();
            parameters.AddParameter("USER_ID", dto.Model.CRET_BY);

            if (!dto.Model.APP_CODE.IsNullOrEmpty())
            {
                strSQL += " AND t.COM_CODE = @APP_CODE";
                parameters.AddParameter("APP_CODE", dto.Model.APP_CODE);
            }

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS01P003Model>();
            }

            return dto;
        }
        private MISS01P003DTO GetDoTask(MISS01P003DTO dto)
        {
            string strSQL = string.Empty;
            strSQL = @"	SELECT t.COM_CODE
	                        ,t.ISE_NO
	                        ,t.RESPONSE_BY
	                        ,t.ISE_DATE_OPENING
	                        ,t.ASSIGN_STATUS
	                        ,tt.COM_NAME_E
                            ,t.ISE_KEY
                        FROM VSMS_COMPANY tt
                        INNER JOIN (
	                        SELECT t.COM_CODE
		                        ,t.ISE_NO
		                        ,tt.RESPONSE_BY
		                        ,t.ISE_DATE_OPENING
		                        ,t.ASSIGN_STATUS
                                ,t.ISE_KEY
	                        FROM VSMS_ISSTATOPSS t
	                        INNER JOIN VSMS_ISSUE tt ON t.COM_CODE = tt.COM_CODE
		                        AND t.ISE_NO = tt.no
	                        WHERE t.USER_ID = @USER_ID
	                        ) t ON t.COM_CODE = tt.COM_CODE
                        AND t.ASSIGN_STATUS in ('D','T')
                        ";


            var parameters = CreateParameter();
            parameters.AddParameter("USER_ID", dto.Model.CRET_BY);

            if (!dto.Model.APP_CODE.IsNullOrEmpty())
            {
                strSQL += " AND t.COM_CODE = @APP_CODE";
                parameters.AddParameter("APP_CODE", dto.Model.APP_CODE);
            }

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS01P003Model>();
            }

            return dto;
        }
        private MISS01P003DTO GetDoneTask(MISS01P003DTO dto)
        {
            string strSQL = string.Empty;
            strSQL = @"	SELECT t.COM_CODE
	                        ,t.ISE_NO
	                        ,t.RESPONSE_BY
	                        ,t.ISE_DATE_OPENING
	                        ,t.ASSIGN_STATUS
	                        ,tt.COM_NAME_E
                            ,t.ISE_KEY
                        FROM VSMS_COMPANY tt
                        INNER JOIN (
	                        SELECT t.COM_CODE
		                        ,t.ISE_NO
		                        ,tt.RESPONSE_BY
		                        ,t.ISE_DATE_OPENING
		                        ,t.ASSIGN_STATUS
                                ,t.ISE_KEY
	                        FROM VSMS_ISSTATOPSS t
	                        INNER JOIN VSMS_ISSUE tt ON t.COM_CODE = tt.COM_CODE
		                        AND t.ISE_NO = tt.no
	                        WHERE t.USER_ID = @USER_ID
	                        ) t ON t.COM_CODE = tt.COM_CODE
                        AND t.ASSIGN_STATUS in ('E','C')
                        ";


            var parameters = CreateParameter();
            parameters.AddParameter("USER_ID", dto.Model.CRET_BY);

            if (!dto.Model.APP_CODE.IsNullOrEmpty())
            {
                strSQL += " AND t.COM_CODE = @APP_CODE";
                parameters.AddParameter("APP_CODE", dto.Model.APP_CODE);
            }

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS01P003Model>();
            }

            return dto;
        }
        private MISS01P003DTO GetFilePacket(MISS01P003DTO dto)
        {


            return dto;
        }
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MISS01P003DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                //case MISS01P003ExecuteType.Insert: return Insert(dto);
            }
            return dto;
        }
        private MISS01P003DTO Insert(MISS01P003DTO dto)
        {
            return dto;
        }
        #endregion

        #region ====Update==========
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MISS01P003DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P003ExecuteType.ConfirmStatus: return ConfirmStatus(dto);
                case MISS01P003ExecuteType.UpdateSolutionResult: return UpdateSolutionResult(dto);
            }
            return dto;
        }
        private MISS01P003DTO ConfirmStatus(MISS01P003DTO dto)
        {
            var parameters = CreateParameter();
            parameters.AddParameter("error_code", null, ParameterDirection.Output);
            parameters.AddParameter("ISE_KEY", dto.Model.ISE_KEY);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("FALG", dto.Model.FALG);
            parameters.AddParameter("NO", dto.Model.ISE_NO);
            parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
            parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);

            var result = _DBMangerNoEF.ExecuteDataSet("[bond].[SP_VSMS_ISSTATOPSS_003]", parameters);
            if (!result.Status)
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = result.ErrorMessage;
                dto.Model.FALG = "F";
            }
            else
            {
                if (result.OutputData["error_code"].ToString().Trim() != "0")
                {
                    dto.Result.IsResult = false;
                    dto.Result.ResultMsg = result.OutputData["error_code"].ToString().Trim();
                    dto.Model.FALG = result.OutputData["error_code"].ToString().Trim();
                }
                else
                {
                    dto.Model.FALG = "T";
                }
            }

            return dto;
        }
        private MISS01P003DTO UpdateSolutionResult(MISS01P003DTO dto)
        {
            string strSQL = @"	update VSMS_ISSUE set SOLUTION = @SOLUTION 
                                        where COM_CODE = @COM_CODE and NO = @NO";
            var parameters = CreateParameter();
            parameters.AddParameter("SOLUTION", dto.Model.SOLUTION);
            parameters.AddParameter("NO", dto.Model.ISE_NO);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (!result.Status)
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = result.ErrorMessage;
            }


            return dto;
        }
        #endregion

        #region ====Delete==========
        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (MISS02P001DTO)baseDTO;
            if (dto.Models.Count() > 0)
            {
                foreach (var item in dto.Models)
                {

                }
            }

            return dto;
        }
        #endregion
    }
}