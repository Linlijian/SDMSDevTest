using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MIS
{
    public class MISS01P002DA : BaseDA
    {
        public MISS01P002DTO DTO { get; set; }

        #region ====Property========
        public MISS01P002DA()
        {
            DTO = new MISS01P002DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MISS01P002DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P002ExecuteType.GetAllAssign: return GetAllAssign(dto);
                case MISS01P002ExecuteType.GetAssignment: return GetFilePacket(dto);
                case MISS01P002ExecuteType.GetFilePacket: return GetFilePacket(dto);
            }
            return dto;
        }
        private MISS01P002DTO GetAllAssign(MISS01P002DTO dto)
        {
            string strSQL = @"	SELECT NO,
                                ISSUE_DATE,
                                RESPONSE_BY,
                                case ISNULL(ASSIGN_USER,'') 
                                when  NULL then 'No assignment' 
                                when '' then 'No assignment' 
                                else ASSIGN_USER end ASSIGN_USER,
                                STATUS
                                FROM VSMS_ISSUE
                                WHERE COM_CODE = @COM_CODE";
            var parameters = CreateParameter();
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);

            if (!dto.Model.NO.IsNullOrEmpty())
            {
                strSQL += " AND NO  >= @NO";
                parameters.AddParameter("NO", dto.Model.NO);
            }
            if (!dto.Model.ISSUE_DATE.IsNullOrEmpty())
            {
                strSQL += " AND ISSUE_DATE >= @ISSUE_DATE";
                parameters.AddParameter("ISSUE_DATE", dto.Model.ISSUE_DATE);
            }
            if (!dto.Model.RESPONSE_BY.IsNullOrEmpty())
            {
                strSQL += " AND RESPONSE_BY like @RESPONSE_BY";
                parameters.AddParameter("RESPONSE_BY", dto.Model.RESPONSE_BY);
            }
            if (!dto.Model.ASSIGN_USER.IsNullOrEmpty())
            {
                strSQL += " AND ASSIGN_USER =  @ASSIGN_USER";
                parameters.AddParameter("ASSIGN_USER", dto.Model.ASSIGN_USER);
            }
            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS01P002Model>();
            }

            return dto;
        }
        private MISS01P002DTO GetFilePacket(MISS01P002DTO dto)
        {
            string strSQL = @"	SELECT *
                                FROM VSMS_ISSUE
                                WHERE COM_CODE = @COM_CODE
                                AND NO = @NO";
            var parameters = CreateParameter();
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("NO", dto.Model.NO);

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Model = result.OutputDataSet.Tables[0].ToObject<MISS01P002Model>();
            }

            return dto;
        }

        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MISS01P002DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P002ExecuteType.Insert: return Insert(dto);
            }
            return dto;
        }
        private MISS01P002DTO Insert(MISS01P002DTO dto)
        {
            
            return dto;
        }
        #endregion

        #region ====Update==========
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MISS01P002DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P002ExecuteType.Update: return Update(dto);
                case MISS01P002ExecuteType.UpdateAssignment: return UpdateAssignment(dto);
                case MISS01P002ExecuteType.UpdateFilePacket: return UpdateFilePacket(dto);
            }
            return dto;
        }
        private MISS01P002DTO Update(MISS01P002DTO dto)
        {

            return dto;
        }
        private MISS01P002DTO UpdateFilePacket(MISS01P002DTO dto)
        {
            var parameters = CreateParameter();

            parameters.AddParameter("error_code", null, ParameterDirection.Output);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("FILE_ID", dto.Model.FILE_ID);
            parameters.AddParameter("NO", dto.Model.NO);
            parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
            parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);

            var result = _DBMangerNoEF.ExecuteDataSet("[bond].[SP_VSMS_ISSUE_003]", parameters, CommandType.StoredProcedure);

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
        private MISS01P002DTO UpdateAssignment(MISS01P002DTO dto)
        {
            var parameters = CreateParameter();

            parameters.AddParameter("error_code", null, ParameterDirection.Output);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters.AddParameter("ASSIGN_USER", dto.Model.ASSIGN_USER);
            parameters.AddParameter("NO", dto.Model.NO);
            parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
            parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);

            var result = _DBMangerNoEF.ExecuteDataSet("[bond].[SP_VSMS_ISSUE_002]", parameters, CommandType.StoredProcedure);

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